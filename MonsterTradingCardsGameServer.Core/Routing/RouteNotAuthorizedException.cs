using System;

namespace MonsterTradingCardsGameServer.Core.Routing
{
    /// <summary>
    /// Exception in case route is not authorized
    /// </summary>
    internal class RouteNotAuthorizedException : Exception
    {
        public RouteNotAuthorizedException()
        {
        }

        public RouteNotAuthorizedException(string message) : base(message)
        {
        }

        public RouteNotAuthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}