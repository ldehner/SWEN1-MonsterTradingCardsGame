using System.Collections.Generic;
using System.Net;
using System.Threading;
using MonsterTradingCardsGameServer.Core.Client;
using MonsterTradingCardsGameServer.Core.Listener;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Core.Routing;
using HttpListener = MonsterTradingCardsGameServer.Core.Listener.HttpListener;

namespace MonsterTradingCardsGameServer.Core.Server
{
    /// <summary>
    /// Http Server, accepts and handles client and creates a thread for each client
    /// </summary>
    public class HttpServer : IServer
    {
        private readonly IListener _listener;
        private readonly IRouter _router;
        private bool _isListening;
        private readonly List<Thread> _threads;

        /// <summary>
        /// Sets all attributes
        /// </summary>
        /// <param name="address">ip address of the server</param>
        /// <param name="port">port of the server</param>
        /// <param name="router">the specific router</param>
        public HttpServer(IPAddress address, int port, IRouter router)
        {
            _listener = new HttpListener(address, port);
            _router = router;
            _threads = new List<Thread>();
        }

        /// <summary>
        /// Starts the server and listens for new clients
        /// </summary>
        public void Start()
        {
            _listener.Start();
            _isListening = true;

            while (_isListening)
            {
                var client = _listener.AcceptClient();
                //HandleClient(client);
                var thread = new Thread(() => HandleClient(client));
                _threads.Add(thread);
                thread.Start();
            }

            _threads.ForEach(thread => thread.Join());
        }

        /// <summary>
        /// stops the server
        /// </summary>
        public void Stop()
        {
            _isListening = false;
            _listener.Stop();
        }

        /// <summary>
        /// Handles a client
        /// </summary>
        /// <param name="client">client</param>
        private void HandleClient(IClient client)
        {
            var request = client.ReceiveRequest();

            Response.Response response;
            try
            {
                var command = _router.Resolve(request);
                if (command != null)
                    response = command.Execute();
                else
                    response = new Response.Response
                    {
                        StatusCode = StatusCode.BadRequest
                    };
            }
            catch (RouteNotAuthorizedException)
            {
                response = new Response.Response
                {
                    StatusCode = StatusCode.Unauthorized
                };
            }

            client.SendResponse(response);
        }
    }
}