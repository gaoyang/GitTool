using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using GitTool.CLI;
using GitTool.Common;
using WPFFolderBrowser;

namespace GitTool
{
    public class MainWindow_Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string RepositoryPath { get; set; } = @"D:\SourceCode\Me\lgy-studio";
        public string OldEmail { get; set; }
        public string NewName { get; set; }
        public string NewEmail { get; set; }
        public KeyValuePair<Brush, string> Message { get; set; }

        public ICommand SelectPathCommand => new Command(OnSelectPath);

        public ICommand UpdateCommand => new Command(OnUpdate);

        void OnSelectPath()
        {
            var dialog = new WPFFolderBrowserDialog();
            dialog.ShowDialog();
            RepositoryPath = dialog.FileName;
        }

        public void OnUpdate()
        {
            Message = default;
            try
            {
                var res = Program.Run(new Params
                {
                    RepositoryPath = RepositoryPath,
                    Email = OldEmail,
                    NewAuthor = $"{NewName} <{NewEmail}>"
                });
                Message = new KeyValuePair<Brush, string>(res.success ? Brushes.ForestGreen : Brushes.IndianRed, res.message);
            }
            catch (Exception e)
            {
                Message = new KeyValuePair<Brush, string>(Brushes.IndianRed, e.Message);
            }
        }
    }
}
