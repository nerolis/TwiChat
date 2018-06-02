using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        API Twitch = new API();

        public MainWindow()

        {
            InitializeComponent();
        }

        private void JoinChannel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChatInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void SendMsgToChat_Click(object sender, RoutedEventArgs e)
        {
            if (ChatInput.Text == "")
            {
                MessageBox.Show("You must write something before send");
            } else
            {
                Twitch.SendMsgToChat(ChatInput.Text);
                MsgBox.Text = MsgBox.Text + "\n" + ChatInput.Text;
                ChatInput.Text = "";
            }   
        }
    }
}
