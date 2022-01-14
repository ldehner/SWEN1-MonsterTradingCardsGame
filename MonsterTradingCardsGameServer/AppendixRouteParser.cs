using System.Collections.Generic;
using System.Text.RegularExpressions;
using MonsterTradingCardsGameServer.Core.Request;
using MonsterTradingCardsGameServer.Core.Routing;

namespace MonsterTradingCardsGameServer
{
    public class AppendixRouteParser : IRouteParser
    {
        public bool IsMatch(RequestContext request, HttpMethod method, string routePattern)
        {
            if (method != request.Method)
            {
                return false;
            }

            var pattern = "^" + routePattern.Replace("{appendix}", ".*").Replace("/", "\\/") + "$";
            return Regex.IsMatch(request.ResourcePath, pattern);
        }

        public Dictionary<string, string> ParseParameters(RequestContext request, string routePattern)
        {
            var parameters = new Dictionary<string, string>();
            var pattern = "^" + routePattern.Replace("{appendix}", "(?<appendix>.*)").Replace("/", "\\/") + "$";

            var result = Regex.Match(request.ResourcePath, pattern);
            if (result.Groups["appendix"].Success)
            {
                parameters["appendix"] = result.Groups["appendix"].Captures[0].Value;
            }

            return parameters;
        }
    }
}