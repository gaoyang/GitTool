using System;
using System.IO;
using CommandLine;

namespace GitTool.CLI
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Parser.Default.ParseArguments<Params>(args).WithParsed(p =>
                {
                    var res = Run(p);
                    Console.WriteLine(res.message);
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static (bool success, string message) Run(Params @params)
        {
            if (!Directory.Exists(@params.RepositoryPath)) throw new ArgumentException("仓库目录不存在");
            var git = new Git(@params.RepositoryPath);
            return git.UpdateAuthor(@params.Email, new GitAuthor(@params.NewAuthor));
        }
    }
}
