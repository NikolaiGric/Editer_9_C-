using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;

namespace Editer_9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static string Path { get; private set; }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Create create = new Create();
            Close();
            create.Show();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                Path = dialog.FileName;
                Create create = new Create();
                Close();
                create.Show();
            }
        }
    }
}
