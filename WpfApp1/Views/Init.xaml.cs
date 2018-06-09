using System;
using System.Windows;
using WpfApp1.Twitch;

namespace WpfApp1.Views
{
    public partial class Init : Window
    {

        MainWindow main = new MainWindow();

        public Init()
        {
            InitializeComponent();
        }

        private void JoinChannel(string _channelName) {
            main.Connect(_channelName);
            Hide();
        }

        private void ConnectChannel_Click(object sender, RoutedEventArgs e)
        {
            string _channelName = ChannelNameInput.Text;
            JoinChannel(_channelName);
        }
    }
}
