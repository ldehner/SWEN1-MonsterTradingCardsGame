using System.Net;
using System.Net.Sockets;
using MonsterTradingCardsGameServer.Core.Client;

namespace MonsterTradingCardsGameServer.Core.Listener
{
    /// <summary>
    ///     Listens to new requesting clients
    /// </summary>
    public class HttpListener : IListener
    {
        private readonly TcpListener _listener;

        /// <summary>
        ///     sets address and port
        /// </summary>
        /// <param name="address">address</param>
        /// <param name="port">port</param>
        public HttpListener(IPAddress address, int port)
        {
            _listener = new TcpListener(address, port);
        }

        /// <summary>
        ///     Accepts a new client
        /// </summary>
        /// <returns>the client</returns>
        public IClient AcceptClient()
        {
            var client = _listener.AcceptTcpClient();
            return new HttpClient(client);
        }

        /// <summary>
        ///     starts the listener
        /// </summary>
        public void Start()
        {
            _listener.Start();
        }

        /// <summary>
        ///     stops the listener
        /// </summary>
        public void Stop()
        {
            _listener.Stop();
        }
    }
}