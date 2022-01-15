using System.Net;
using System.Net.Sockets;
using MonsterTradingCardsGameServer.Core.Client;

namespace MonsterTradingCardsGameServer.Core.Listener
{
    public class HttpListener : IListener
    {
        private readonly TcpListener listener;

        public HttpListener(IPAddress address, int port)
        {
            listener = new TcpListener(address, port);
        }


        public IClient AcceptClient()
        {
            var client = listener.AcceptTcpClient();
            return new HttpClient(client);
        }

        public void Start()
        {
            listener.Start();
        }

        public void Stop()
        {
            listener.Stop();
        }
    }
}