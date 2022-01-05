using MonsterTradingCardsGameServer.Core.Request;

namespace MonsterTradingCardsGameServer.Core.Routing
{
    public interface IRouter
    {
        IRouteCommand Resolve(RequestContext request);
    }
}
