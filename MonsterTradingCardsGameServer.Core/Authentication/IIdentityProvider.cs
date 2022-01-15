using MonsterTradingCardsGameServer.Core.Request;

namespace MonsterTradingCardsGameServer.Core.Authentication
{
    public interface IIdentityProvider
    {
        IIdentity GetIdentyForRequest(RequestContext request);
    }
}