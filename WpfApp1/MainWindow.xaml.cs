using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        Api Twitch = new Api();

        public MainWindow()

        {
            InitializeComponent();
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
