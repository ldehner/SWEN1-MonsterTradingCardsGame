using System.Collections.Generic;
using System.Text.RegularExpressions;
using MonsterTradingCardsGameServer.Core.Request;
using MonsterTradingCardsGameServer.Core.Routing;

namespace MonsterTradingCardsGameServer
{
    /// <summary>
    /// checks for an appendix in the route
    /// </summary>
    public class AppendixRouteParser : IRouteParser
    {
        /// <summary>
        /// checks if the appendix is a match
        /// </summary>
        /// <param name="request">the request context</param>
        /// <param name="method">the http method</param>
        /// <param name="routePattern">the route pattern</param>
        /// <returns>if appendix is a match</returns>
        public bool IsMatch(RequestContext request, HttpMethod method, string routePattern)
        {
            if (method != request.Method) return false;

            var pattern = "^" + routePattern.Replace("{appendix}", ".*").Replace("/", "\\/") + "$";
            return Regex.IsMatch(request.ResourcePath, pattern);
        }

        /// <summary>
        /// parses the parameters
        /// </summary>
        /// <param name="request">the request context</param>
        /// <param name="routePattern">the route pattern</param>
        /// <returns>the parsed parameters as dictionary</returns>
        public Dictionary<string, string> ParseParameters(RequestContext request, string routePattern)
        {
            var parameters = new Dictionary<string, string>();
            var pattern = "^" + routePattern.Replace("{appendix}", "(?<appendix>.*)").Replace("/", "\\/") + "$";

            var result = Regex.Match(request.ResourcePath, pattern);
            if (result.Groups["appendix"].Success) parameters["appendix"] = result.Groups["appendix"].Captures[0].Value;

            return parameters;
        }
    }
}