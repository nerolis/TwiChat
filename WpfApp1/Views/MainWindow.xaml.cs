using System;
using System.Windows;
using WpfApp1.Views;
using WpfApp1.Twitch;
using System.Threading;


namespace WpfApp1
{
    public partial class MainWindow : Window
    {

        private static System.Timers.Timer aTimer;

        Client Irc;

        public MainWindow()
        {
            Irc = new Client("irc.twitch.tv", 6667, Properties.Settings.Default.botName, Properties.Settings.Default.twitchToken, channelName);
            InitializeComponent();
        }


        private void StartPool()
        {
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000;
            aTimer.Elapsed += ReadMsgsTimer;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void ReadMsgsTimer(Object source, System.Timers.ElapsedEventArgs e)
        {
            string message = Irc.ReadMessage();

            HandleEvents(message);
        }

        public void Connect(string channelName)
        {

            Sender ping = new Sender(Irc);

            ping.Start();

            if (!Irc.IsConnected())
                return;

            StartPool();
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
