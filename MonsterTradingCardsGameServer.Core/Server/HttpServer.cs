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
    public class HttpServer : IServer
    {
        private readonly IListener _listener;
        private readonly IRouter _router;
        private bool _isListening;
        private readonly List<Thread> _threads;

        public HttpServer(IPAddress address, int port, IRouter router)
        {
            _listener = new HttpListener(address, port);
            _router = router;
            _threads = new List<Thread>();
        }

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

        public void Stop()
        {
            _isListening = false;
            _listener.Stop();
        }

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