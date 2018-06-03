using System;
using System.Windows;
using WpfApp1.Twitch;
using System.Threading;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private static string _botName = "qwhejuqwhne";
        private static string _twitchOAuth = "oauth:prntuf0vfymzwfcohj0htd6kt9blcc";
        private static string _channelName;

        Client irc;

        private static System.Timers.Timer aTimer;

        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SetTimerAndStartPing(string _channelName)
        {
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000; 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            irc = new Client("irc.twitch.tv", 6667, _botName, _twitchOAuth, _channelName);

            Sender ping = new Sender(irc);
            ping.Start();

            if (irc.IsConnected())
            {
                ChatInput.IsEnabled = true;
                SendMsgToChat.IsEnabled = true;
                ChatInput.Text = "";
                CurrentChannel.Content = "Current channel: " + _channelName;
                ChannelName.Text = "";
            } else
            {
                MessageBox.Show("Err");
            }
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            HandleEvents();
        }

        private void HandleEvents()
        {
            string message = irc.ReadMessage();

            if (message.Contains("PRIVMSG"))
            {
                int intIndexParseSign = message.IndexOf('!');
                string userName = message.Substring(1, intIndexParseSign - 1);
                intIndexParseSign = message.IndexOf(" :");
                message = message.Substring(intIndexParseSign + 2);

                string parsedMsg = userName + ": " + message;

                showMsg(parsedMsg);
            }
               

        }

        private void SendMsgToChat_Click(object sender, RoutedEventArgs e)
        {
            if (ChatInput.Text == "" && !irc.IsConnected())
            {
                MessageBox.Show("You must write something before send");
            }
            else
            {
                irc.SendPublicChatMessage(ChatInput.Text);
                MsgBox.Text = MsgBox.Text + "\n" + _botName + ": " + ChatInput.Text;
                ChatInput.Text = "";
            }
        }

        private void showMsg(string msg)
        {   
             Dispatcher.BeginInvoke(new ThreadStart(delegate { MsgBox.Text = MsgBox.Text + "\n" + msg; }));
        }

        private void ConnectChannel_Click(object sender, RoutedEventArgs e)
        {
            string _channelName = ChannelName.Text;
            SetTimerAndStartPing(_channelName);
        }

        private void ChatInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
