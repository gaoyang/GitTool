using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using GitTool.Common;
using LibGit2Sharp;

namespace GitTool
{
    public class MainWindow_Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string RepositoryPath { get; set; } = @"D:\SourceCode\Me\lgy-studio";
        public string OldEmail { get; set; }
        public string NewName { get; set; }
        public string NewEmail { get; set; }

        public ICommand UpdateCommand => new Command(OnUpdate);

        public void OnUpdate()
        {
            var repository = new Repository(RepositoryPath);
            var commitsToRewrite = repository.Commits;
            var identity = new Identity(NewName, NewEmail);

            repository.Refs.RewriteHistory(new RewriteHistoryOptions
            {
                BackupRefsNamespace = "test111",
                CommitHeaderRewriter = commit =>
                {
                    if (commit.Author.Email == OldEmail) return CommitRewriteInfo.From(commit, new Signature(identity, DateTimeOffset.Now));
                    Debug.WriteLine(commit.Author.Name);
                    return CommitRewriteInfo.From(commit);
                },
                OnError = OnError,
                OnSucceeding = OnSucceeding,
            }, commitsToRewrite);
        }

        void OnSucceeding()
        {
            Debug.WriteLine("OnSucceeding");
        }

        void OnError(Exception obj)
        {
            Debug.WriteLine("OnError");
        }
    }
}
