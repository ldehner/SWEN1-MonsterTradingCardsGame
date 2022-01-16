using MonsterTradingCardsGameServer.Core.Request;

namespace MonsterTradingCardsGameServer.Core.Routing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRouter
    {
        /// <summary>
        /// resolves the request context
        /// </summary>
        /// <param name="request">request</param>
        /// <returns>the route command</returns>
        IRouteCommand Resolve(RequestContext request);
    }
}