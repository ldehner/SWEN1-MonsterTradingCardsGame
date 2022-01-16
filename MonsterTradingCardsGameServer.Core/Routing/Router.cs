using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Core.Authentication;
using MonsterTradingCardsGameServer.Core.Request;

namespace MonsterTradingCardsGameServer.Core.Routing
{
    /// <summary>
    /// Resolves the routes
    /// </summary>
    public class Router : IRouter
    {
        public delegate IProtectedRouteCommand CreateProtectedRouteCommand(RequestContext request,
            Dictionary<string, string> parameters);

        public delegate IRouteCommand CreatePublicRouteCommand(RequestContext request,
            Dictionary<string, string> parameters);

        private readonly IIdentityProvider _identityProvider;

        private readonly IRouteParser _routeParser;

        private readonly Dictionary<Tuple<HttpMethod, string>, ICreator> _routes;

        /// <summary>
        /// Sets all attributes
        /// </summary>
        /// <param name="routeParser">the route parser</param>
        /// <param name="identityProvider">the identity provider</param>
        public Router(IRouteParser routeParser, IIdentityProvider identityProvider)
        {
            _routes = new Dictionary<Tuple<HttpMethod, string>, ICreator>();
            _routeParser = routeParser;
            _identityProvider = identityProvider;
        }

        /// <summary>
        /// resolves a route
        /// </summary>
        /// <param name="request">users request</param>
        /// <returns>the route command</returns>
        /// <exception cref="NotImplementedException">exception in case route is not implemented</exception>
        public IRouteCommand Resolve(RequestContext request)
        {
            IRouteCommand command = null;

            foreach (var route in _routes.Keys)
                if (_routeParser.IsMatch(request, route.Item1, route.Item2))
                {
                    var parameters = _routeParser.ParseParameters(request, route.Item2);
                    var creator = _routes[route];
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

        /// <summary>
        /// Adds a new route to the router
        /// </summary>
        /// <param name="method">the http method</param>
        /// <param name="routePattern">the router pattern</param>
        /// <param name="create">the route command</param>
        public void AddRoute(HttpMethod method, string routePattern, CreatePublicRouteCommand create)
        {
            var key = new Tuple<HttpMethod, string>(method, routePattern);
            var value = new PublicCreator {Create = create};
            _routes.Add(key, value);
        }

        /// <summary>
        /// Adds a new protected route to the router
        /// </summary>
        /// <param name="method">the http method</param>
        /// <param name="routePattern">the route pattern</param>
        /// <param name="create">rhe route command</param>
        public void AddProtectedRoute(HttpMethod method, string routePattern, CreateProtectedRouteCommand create)
        {
            var key = new Tuple<HttpMethod, string>(method, routePattern);
            var value = new ProtectedCreator {Create = create};
            _routes.Add(key, value);
        }

        /// <summary>
        /// Protects a command
        /// </summary>
        /// <param name="create">the command</param>
        /// <param name="request">users request</param>
        /// <param name="parameters">requests parameters</param>
        /// <returns>the protected route command</returns>
        /// <exception cref="RouteNotAuthorizedException">exception in case user is not authorized</exception>
        private IProtectedRouteCommand Protect(CreateProtectedRouteCommand create, RequestContext request,
            Dictionary<string, string> parameters)
        {
            var identity = _identityProvider.GetIdentityForRequest(request);

            var command = create(request, parameters);
            command.Identity = identity ?? throw new RouteNotAuthorizedException();

            return command;
        }

        /// <summary>
        /// ICreator Interface
        /// </summary>
        private interface ICreator
        {
        }

        /// <summary>
        /// Creates Public Route Command
        /// </summary>
        private class PublicCreator : ICreator
        {
            public CreatePublicRouteCommand Create { get; set; }
        }

        /// <summary>
        /// Creates Protected Route Command
        /// </summary>
        private class ProtectedCreator : ICreator
        {
            public CreateProtectedRouteCommand Create { get; set; }
        }
    }
}