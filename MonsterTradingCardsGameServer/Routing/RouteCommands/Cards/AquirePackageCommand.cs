using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Cards
{
    public class AquirePackageCommand : ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;

        public AquirePackageCommand(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public override Response Execute()
        {
            return !_userManager.AquirePackage(User.Username)
                ? new Response {StatusCode = StatusCode.Conflict}
                : new Response {StatusCode = StatusCode.Ok};
        }
    }
}