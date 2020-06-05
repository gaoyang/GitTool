using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using GitTool.CLI;
using GitTool.Common;
using WPFFolderBrowser;
using XC.RSAUtil;

namespace GitTool
{
    public class MainWindow_Model : INotifyPropertyChanged
    {
        public string RepositoryPath { get; set; } = @"D:\SourceCode\Me\lgy-studio";
        public string OldEmail { get; set; }
        public string NewName { get; set; }
        public string NewEmail { get; set; }
        public KeyValuePair<Brush, string> Message { get; set; }

        public ICommand SelectPathCommand => new Command(OnSelectPath);
        public ICommand UpdateCommand => new Command(OnUpdate);
        public ICommand FixedMergeCommand => new Command(OnFixedMerge);
        public ICommand GenRsaKeyCommand => new Command(OnGenRsaKey);
        public event PropertyChangedEventHandler PropertyChanged;

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

        void OnFixedMerge()
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

        void OnGenRsaKey()
        {
            var dialog = new WPFFolderBrowserDialog();
            dialog.ShowDialog();

            var keyDir = dialog.FileName;
            var xmlKey = RsaKeyGenerator.Pkcs8Key(2048, true);

            File.WriteAllText(Path.Combine(keyDir, $"{DateTime.Now:yyyyMMddHHmmssfff}.private"), xmlKey[0]);
            File.WriteAllText(Path.Combine(keyDir, $"{DateTime.Now:yyyyMMddHHmmssfff}.public"), xmlKey[1]);

            Process.Start("explorer.exe", keyDir);
            // new RsaXmlUtil().PrivateRsa.
            
        }
    }
}