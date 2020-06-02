using System.Linq;
using System.Windows;

namespace GitTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AllowDrop = true;
            Drop += OnDrop;
        }

        void OnDrop(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop) as string[] ?? new string[] { };
            path.Text = files.FirstOrDefault();
        }
    }
}
