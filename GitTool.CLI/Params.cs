using CommandLine;

namespace GitTool.CLI
{
    public class Params
    {
        [Option('p', "path", Required = true, HelpText = "仓库地址")]
        public string RepositoryPath { get; set; }

        [Option('e', "email", Required = true, HelpText = "需要修改的邮箱")]
        public string Email { get; set; }

        [Option('a', "author", Required = true, HelpText = "新提交者信息,例如: \"name <email>\" ")]
        public string NewAuthor { get; set; }
    }
}
