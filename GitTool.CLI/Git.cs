using System;
using LibGit2Sharp;

namespace GitTool.CLI
{
    public class Git
    {
        readonly string repositoryPath;

        public Git(string repositoryPath)
        {
            this.repositoryPath = repositoryPath;
        }

        public (bool success, string message) UpdateAuthor(string email, GitAuthor newAuthor)
        {
            if (string.IsNullOrEmpty(email)) return (false, "未指定邮箱地址");
            var repository = new Repository(repositoryPath);
            var message = "失败";
            var success = false;
            var editCommitCount = 0;
            repository.Refs.RewriteHistory(new RewriteHistoryOptions
            {
                BackupRefsNamespace = $"refs/heads/backup/{DateTime.Now:yyMMddHHmmsss}/",
                CommitHeaderRewriter = commit =>
                {
                    var info = CommitRewriteInfo.From(commit);
                    if (info.Author.Email == email && info.Author.Name != newAuthor.Name)
                    {
                        info.Author = new Signature(newAuthor.Name, newAuthor.Email, DateTimeOffset.Now);
                        editCommitCount++;
                        return info;
                    }

                    return null;
                },
                OnError = ex =>
                {
                    message = ex.Message;
                    success = false;
                },
                OnSucceeding = () =>
                {
                    message = $"成功,共有{editCommitCount}个commit被更新";
                    success = true;
                }
            }, repository.Commits);

            return (success, message);
        }
    }
}
