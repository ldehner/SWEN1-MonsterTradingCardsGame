using MonsterTradingCardsGameServer.Core.Authentication;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Core.Routing;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.RouteCommands
{
    public abstract class ProtectedRouteCommand : IProtectedRouteCommand
    {
        public User User => (User) Identity;
        public IIdentity Identity { get; set; }

        public abstract Response Execute();
    }
}