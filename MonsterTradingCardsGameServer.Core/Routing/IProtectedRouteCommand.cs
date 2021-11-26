using MonsterTradingCardsGameServer.Core.Authentification;

namespace MonsterTradingCardsGameServer.Core.Routing
{
    public interface IProtectedRouteCommand:IRouteCommand
    {
        IIdentity Identity { get; set; }
    }
}