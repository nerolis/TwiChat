using System;
using System.Windows;
using WpfApp1.Views;
using WpfApp1.Twitch;
using System.Threading;


namespace WpfApp1
{
    public partial class MainWindow : Window
    {


        Client Irc;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void HandleEvents(string message)
        {
    
            if (message.Contains("PRIVMSG"))
            {
                int intIndexParseSign = message.IndexOf('!');
                string userName = message.Substring(1, intIndexParseSign - 1);
                intIndexParseSign = message.IndexOf(" :");
                message = message.Substring(intIndexParseSign + 2);

                string parsedMsg = userName + ": " + message;

                ShowMsg(parsedMsg);
            }
        }

    
        private void ShowMsg(string msg)
        {   
             Dispatcher.BeginInvoke(new ThreadStart(delegate { MsgBox.Text = MsgBox.Text + "\n" + msg; }));
        }

        private void SendMsgToChat_Click(object sender, RoutedEventArgs e)
        {
            if (ChatInput.Text == "" && !Irc.IsConnected())
            {
                MessageBox.Show("You must write something before send");
            }
            else
            {
                Irc.SendPublicChatMessage(ChatInput.Text);
                MsgBox.Text = MsgBox.Text + "\n" + "test" + ": " + ChatInput.Text;
                ChatInput.Text = "";
            }
        }

    }
}
