using System.Collections.Generic;
using System.Net;
using System.Threading;
using MonsterTradingCardsGameServer.Core.Client;
using MonsterTradingCardsGameServer.Core.Listener;
using MonsterTradingCardsGameServer.Core.Routing;

namespace MonsterTradingCardsGameServer.Core.Server
{
    public class HttpServer : IServer
    {
        private readonly IListener listener;
        private readonly IRouter router;
        private bool isListening;
        private List<Thread> threads;

        public HttpServer(IPAddress address, int port, IRouter router)
        {
            listener = new Listener.HttpListener(address, port);
            this.router = router;
            threads = new List<Thread>();
        }

        public void Start()
        {
            listener.Start();
            isListening = true;

            while (isListening)
            {
                
                var client = listener.AcceptClient();
                //HandleClient(client);
                var thread = new Thread(() => HandleClient(client));
                threads.Add(thread);
                thread.Start();
            }
            
            threads.ForEach(thread => thread.Join());
        }

        public void Stop()
        {
            isListening = false;
            listener.Stop();
        }

        private void HandleClient(IClient client)
        {
            var request = client.ReceiveRequest();

            Response.Response response;
            try
            {
                var command = router.Resolve(request);
                if (command != null)
                {
                    response = command.Execute();
                }
                else
                {
                    response = new Response.Response()
                    {
                        StatusCode = Response.StatusCode.BadRequest
                    };
                }
            }
            catch (RouteNotAuthorizedException)
            {
                response = new Response.Response()
                {
                    StatusCode = Response.StatusCode.Unauthorized
                };
            }

            client.SendResponse(response);
        }
    }
}
