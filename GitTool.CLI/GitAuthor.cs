using System;
using System.Text.RegularExpressions;

namespace GitTool.CLI
{
    public class GitAuthor
    {
        readonly Regex regex = new Regex(@"^(.*) <(.*)>$");

        public GitAuthor(string author)
        {
            var match = regex.Match(author);
            if (match.Groups.Count != 3) throw new ArgumentException("无法识别的参数", nameof(author));

            Name = match.Groups[1].Value;
            Email = match.Groups[2].Value;
        }

        public string Name { get; }
        public string Email { get; }
    }
}
