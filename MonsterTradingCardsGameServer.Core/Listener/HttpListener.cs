using System.Net;
using System.Net.Sockets;
using MonsterTradingCardsGameServer.Core.Client;

namespace MonsterTradingCardsGameServer.Core.Listener
{
    public class HttpListener : IListener
    {
        private readonly TcpListener _listener;

        public HttpListener(IPAddress address, int port)
        {
            _listener = new TcpListener(address, port);
        }


        public IClient AcceptClient()
        {
            var client = _listener.AcceptTcpClient();
            return new HttpClient(client);
        }

        public void Start()
        {
            _listener.Start();
        }

        public void Stop()
        {
            _listener.Stop();
        }
    }
}