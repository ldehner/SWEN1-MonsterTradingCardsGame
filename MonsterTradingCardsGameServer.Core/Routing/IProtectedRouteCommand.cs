using MonsterTradingCardsGameServer.Core.Authentication;

namespace MonsterTradingCardsGameServer.Core.Routing
{
    public interface IProtectedRouteCommand : IRouteCommand
    {
        IIdentity Identity { get; set; }
    }
}
