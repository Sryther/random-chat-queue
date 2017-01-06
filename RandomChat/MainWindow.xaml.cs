using Microsoft.WindowsAzure.Storage.Queue;
using RandomChat.Azure;
using RandomChat.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RandomChat
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _text;

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Text
        {
            get { return _text; }
            set {
                _text = value;
                OnPropertyChanged("Text");
            }
        }

        public MainWindow()
        {
            AzureConnection.GetInstance().CreateQueue("messages");
            InitializeComponent();
            this.DataContext = this;
            RunMessageFetcher();
        }

        private void SendMessage()
        {
            string message = ChatBox.Text;
            ChatBox.Text = "";

            if (message != "")
            {
                MessageService.GetInstance().Post(message);
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SendMessage();
            }
        }

        private void SendButtonClick(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void RunMessageFetcher()
        {
            var thread = new Thread(() =>
            {
                while (true)
                {
                    string message = MessageService.GetInstance().Get();
                    if (message != null)
                    {
                        Text = Text + "\r\n" + message;
                    }
                    System.Threading.Thread.Sleep(1000);
                }
            });
            thread.Start();
            
        }
    }
}
