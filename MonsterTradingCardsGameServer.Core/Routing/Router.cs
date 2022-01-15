using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Core.Authentication;
using MonsterTradingCardsGameServer.Core.Request;

namespace MonsterTradingCardsGameServer.Core.Routing
{
    public class Router : IRouter
    {
        public delegate IProtectedRouteCommand CreateProtectedRouteCommand(RequestContext request,
            Dictionary<string, string> parameters);

        public delegate IRouteCommand CreatePublicRouteCommand(RequestContext request,
            Dictionary<string, string> parameters);

        private readonly IIdentityProvider identityProvider;

        private readonly IRouteParser routeParser;

        private readonly Dictionary<Tuple<HttpMethod, string>, ICreator> routes;

        public Router(IRouteParser routeParser, IIdentityProvider identityProvider)
        {
            routes = new Dictionary<Tuple<HttpMethod, string>, ICreator>();
            this.routeParser = routeParser;
            this.identityProvider = identityProvider;
        }

        public IRouteCommand Resolve(RequestContext request)
        {
            IRouteCommand command = null;

            foreach (var route in routes.Keys)
                if (routeParser.IsMatch(request, route.Item1, route.Item2))
                {
                    var parameters = routeParser.ParseParameters(request, route.Item2);
                    var creator = routes[route];
                    command = creator switch
                    {
                        PublicCreator c => c.Create(request, parameters),
                        ProtectedCreator c => Protect(c.Create, request, parameters),
                        _ => throw new NotImplementedException()
                    };
                    break;
                }

            return command;
        }

        public void AddRoute(HttpMethod method, string routePattern, CreatePublicRouteCommand create)
        {
            var key = new Tuple<HttpMethod, string>(method, routePattern);
            var value = new PublicCreator {Create = create};
            routes.Add(key, value);
        }

        public void AddProtectedRoute(HttpMethod method, string routePattern, CreateProtectedRouteCommand create)
        {
            var key = new Tuple<HttpMethod, string>(method, routePattern);
            var value = new ProtectedCreator {Create = create};
            routes.Add(key, value);
        }

        private IProtectedRouteCommand Protect(CreateProtectedRouteCommand create, RequestContext request,
            Dictionary<string, string> parameters)
        {
            var identity = identityProvider.GetIdentyForRequest(request);

            var command = create(request, parameters);
            command.Identity = identity ?? throw new RouteNotAuthorizedException();

            return command;
        }

        private interface ICreator
        {
        }

        private class PublicCreator : ICreator
        {
            public CreatePublicRouteCommand Create { get; set; }
        }

        private class ProtectedCreator : ICreator
        {
            public CreateProtectedRouteCommand Create { get; set; }
        }
    }
}