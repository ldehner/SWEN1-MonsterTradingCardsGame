using MonsterTradingCardsGameServer.Core.Authentication;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Core.Routing;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands
{
    public abstract class ProtectedRouteCommand : IProtectedRouteCommand
    {
        public User User => (User) Identity;
        public IIdentity Identity { get; set; }

        /// <summary>
        ///     Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public abstract Response Execute();
    }
}