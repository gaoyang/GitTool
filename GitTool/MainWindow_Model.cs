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

        public string RepositoryPath { get; set; }

        public string OldEmail { get; set; }

        public string NewEmail { get; set; }

        public string NewUserName { get; set; }

        public ICommand UpdateCommand => new Command(OnUpdate);

        public void OnUpdate()
        {
            var repository = new Repository(@"D:\SourceCode\Me\lgy-studio");

            var head = repository.Head;
            var trackedBranch = head.TrackedBranch;
            var identity = new Identity("leo", "xz@lgysh.cn");
            

            // var rebaseResult = repository.Rebase.Start(head, trackedBranch, null, identity, new RebaseOptions());


            Debug.WriteLine("=======Commits====");

            foreach (var commit in repository.Commits)
            {
                Debug.WriteLine(commit.Sha);
                if (commit.Sha == "a8ce505d2abfb7157ce4a98f8b067b4c82f85b69")
                {
                    var cherryPickResult = repository.CherryPick(commit, new Signature(identity, DateTimeOffset.Now));
                    
                }
            }
        }
    }
}
