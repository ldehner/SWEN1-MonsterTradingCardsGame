using System.Collections.Generic;
using MonsterTradingCardsGameServer.Core.Request;

namespace MonsterTradingCardsGameServer.Core.Routing
{
    public interface IRouteParser
    {
        bool IsMatch(RequestContext request, HttpMethod method, string routePattern);
        Dictionary<string, string> ParseParameters(RequestContext request, string routePattern);
    }
}