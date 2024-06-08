using ImapX.Collections;
using ImapX;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.Win32;

namespace Editer_9
{
    /// <summary>
    /// Логика взаимодействия для Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        public string FilePath { get; private set; }
        public Create()
        {
            InitializeComponent();

            FilePath = MainWindow.Path;
            if (FilePath != null)
            {
                Document doc = new Document();
                doc.LoadFromFile(FilePath);
                doc.SaveToFile("есть.rtf", FileFormat.Rtf);

                var range = new TextRange(Myrtb.Document.ContentStart, Myrtb.Document.ContentEnd);
                var fs = new FileStream("есть.rtf", FileMode.OpenOrCreate);
                range.Load(fs, DataFormats.Rtf);
                fs.Close();
            }

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Word файл(*docx)|*.docx";
            if (dialog.ShowDialog() == true)
            {
                string Path = dialog.SafeFileName;

                var range = new TextRange(Myrtb.Document.ContentStart, Myrtb.Document.ContentEnd);
                var fs = new FileStream($"{Path}.rtf", FileMode.Create);
                range.Save(fs, DataFormats.Rtf);
                fs.Close();

                Document d = new Document();
                d.LoadFromFile($"{Path}.rtf");
                d.SaveToFile($"C://Users//nikol//OneDrive//Рабочий стол//{Path}", FileFormat.Docx);
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {

            Mail mail = new Mail();
            Close();
            mail.Show();

        }
    }
}   
