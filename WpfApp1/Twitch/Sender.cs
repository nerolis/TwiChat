using System.Threading;

namespace WpfApp1.Twitch
{
    class Sender
    {
        private Client _Irc;
        private Thread pingSender;

        public Sender(Client irc)
        {
            _Irc = irc;
            pingSender = new Thread(new ThreadStart(this.Run));

        }

        public void Start()
        {
            pingSender.IsBackground = true;
            pingSender.Start();
        }

        public void Run()
        {
      
        _Irc.SendIrcMessage("PING irc.twitch.tv");
         Thread.Sleep(200000);
        }
    }
}
