using MonsterTradingCardsGameServer.Core.Request;

namespace MonsterTradingCardsGameServer.Core.Authentication
{
    /// <summary>
    ///     Provides identity for user
    /// </summary>
    public interface IIdentityProvider
    {
        IIdentity GetIdentityForRequest(RequestContext request);
    }
}