using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace WpfApp1.Twitch
{
    class Client
    {
        TcpClient tcpClient = new TcpClient();

        public Client(int port, string token)
        {

        }

    private void connect()
        {
            tcpClient.Connect("server", 9999);
        }
    }
}
