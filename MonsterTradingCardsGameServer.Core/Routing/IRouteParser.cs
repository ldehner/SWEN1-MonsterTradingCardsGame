using System.Collections.Generic;
using MonsterTradingCardsGameServer.Core.Request;

namespace MonsterTradingCardsGameServer.Core.Routing
{
    /// <summary>
    ///     Interface for route parser
    /// </summary>
    public interface IRouteParser
    {
        /// <summary>
        ///     Checks if route is a match
        /// </summary>
        /// <param name="request">request context</param>
        /// <param name="method">the http method</param>
        /// <param name="routePattern">the route pattern</param>
        /// <returns>if route is a match</returns>
        bool IsMatch(RequestContext request, HttpMethod method, string routePattern);

        /// <summary>
        ///     parses the parameters
        /// </summary>
        /// <param name="request">request context</param>
        /// <param name="routePattern">the route pattern</param>
        /// <returns></returns>
        Dictionary<string, string> ParseParameters(RequestContext request, string routePattern);
    }
}