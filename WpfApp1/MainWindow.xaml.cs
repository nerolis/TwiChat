using System;
using System.Windows;
using WpfApp1.Twitch;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private static string _botName = "qwhejuqwhne";
        private static string _broadcasterName = "qwhejuqwhne";
        private static string _twitchOAuth = "oauth:prntuf0vfymzwfcohj0htd6kt9blcc";

        private static System.Timers.Timer aTimer;

        Client irc = new Client("irc.twitch.tv", 6667, _botName, _twitchOAuth, _broadcasterName);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SetTimerAndStartPing()
        {
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000; 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            Sender ping = new Sender(irc);
            ping.Start();

            if (irc.IsConnected())
            {
                ChatInput.IsEnabled = true;
                ChatInput.Text = "";
                SendMsgToChat.IsEnabled = true;
                MsgBox.Text = "Connected";

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
            if (ChatInput.Text == "")
            {
                MessageBox.Show("You must write something before send");
            }
            else
            {
                irc.SendPublicChatMessage(ChatInput.Text);
                MsgBox.Text = MsgBox.Text + "\n" + ChatInput.Text;
                ChatInput.Text = "";
            }
        }

        private void showMsg(string msg)
        {   
             Dispatcher.BeginInvoke(new ThreadStart(delegate { MsgBox.Text = MsgBox.Text + "\n" + msg; }));
        }

        private void ConnectChannel_Click(object sender, RoutedEventArgs e)
        {   
            SetTimerAndStartPing();
        }
    }
}
