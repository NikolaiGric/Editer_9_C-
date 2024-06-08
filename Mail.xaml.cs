using ImapX.Collections;
using ImapX;
using System;
using System.Collections.Generic;
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
using System.Net.Mail;
using Microsoft.WindowsAPICodePack.Net;
using System.Net;

namespace Editer_9
{
    /// <summary>
    /// Логика взаимодействия для Mail.xaml
    /// </summary>
    public partial class Mail : Window
    {
        public Mail()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextRange range = new TextRange(MessageRtb.Document.ContentStart, MessageRtb.Document.ContentEnd);

            MailMessage message = new MailMessage(From.Text, To.Text, Subject.Text, range.Text);
            message.Attachments.Add(new System.Net.Mail.Attachment("C://Users//nikol//OneDrive//Рабочий стол//ДаСработало.docx"));
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("mail.com");
            client.Credentials = new NetworkCredential(From.Text, Pass.Text);   
            client.EnableSsl = true;
            client.Send(message);
        }


        internal class ImapHelper
        {
            private static ImapClient client { get; set; }


            public static void Initialize(string host)
            {
                client = new ImapClient(host, true);
                if (!client.Connect())
                {
                    throw new Exception("Не удалось подключиться!");
                }
            }
            public static bool Login(string u, string p)
            {
                return client.Login(u, p);
            }
            public static void Logout()
            {
                // Выйти из аккаунта, если он авторизирован.  
                if (client.IsAuthenticated)
                {
                    client.Logout();
                    client.Dispose();
                }
            }
            public static CommonFolderCollection GetFolders()
            {
                client.Folders.Inbox.StartIdling(); // И продолжить слушать входящие дальше.  
                client.Folders.Inbox.OnNewMessagesArrived += Inbox_OnNewMessagesArrived;
                return client.Folders;
            }
            private static void Inbox_OnNewMessagesArrived(object sender, IdleEventArgs e)
            {
                // Показать сообщение  
                MessageBox.Show($"Пришло новое сообщение в папку {e.Folder.Name}.");
            }
            public static MessageCollection GetMessagesForFolder(string name)
            {
                client.Folders[name].Messages.Download(); // Начать скачивание сообщений;  
                return client.Folders[name].Messages;
            }
        }

    }
}
