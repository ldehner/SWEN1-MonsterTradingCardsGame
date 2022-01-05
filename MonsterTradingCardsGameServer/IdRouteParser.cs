using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MonsterTradingCardsGameServer.Core.Request;
using MonsterTradingCardsGameServer.Core.Routing;

namespace MonsterTradingCardsGameServer
{
    class IdRouteParser : IRouteParser
    {
        public bool IsMatch(RequestContext request, HttpMethod method, string routePattern)
        {
            if (method != request.Method)
            {
                return false;
            }

            var pattern = "^" + routePattern.Replace("{username}", ".*").Replace("/", "\\/") + "$";
            return Regex.IsMatch(request.ResourcePath, pattern);
        }

        public Dictionary<string, string> ParseParameters(RequestContext request, string routePattern)
        {
            var parameters = new Dictionary<string, string>();
            var pattern = "^" + routePattern.Replace("{id}", "(?<id>.*)").Replace("/", "\\/") + "$";

            var result = Regex.Match(request.ResourcePath, pattern);
            if (result.Groups["id"].Success)
            {
                parameters["id"] = result.Groups["id"].Captures[0].Value;
            }
            Console.WriteLine(parameters);
            return parameters;
        }
    }
}