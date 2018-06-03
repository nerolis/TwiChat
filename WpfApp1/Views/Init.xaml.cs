using System;
using System.Windows;
using WpfApp1.Views;
using WpfApp1.Twitch;

namespace WpfApp1.Views
{
    public partial class Init : Window
    {
        private static string _botName = "qwhejuqwhne";
        private static string _twitchOAuth = "oauth:prntuf0vfymzwfcohj0htd6kt9blcc";
        private static string _channelName;

        private static System.Timers.Timer aTimer;

        Client irc;
        MainWindow main = new MainWindow();

        public Init()
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

            if (!irc.IsConnected())
                return;

            main.Show();

            this.Hide();
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            string message = irc.ReadMessage();

            main.HandleEvents(message);
        }

        private void ConnectChannel_Click(object sender, RoutedEventArgs e)
        {
            string _channelName = ChannelNameInput.Text;
            SetTimerAndStartPing(_channelName);
        }
    }
}
