using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.RouteCommands.Cards
{
    public class AquirePackageCommand : ProtectedRouteCommand
    {
        private readonly User _currentUser;
        private readonly IUserManager _userManager;

        public AquirePackageCommand(IUserManager userManager, User currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public override Response Execute()
        {
            return !_userManager.AquirePackage(_currentUser.Username)
                ? new Response {StatusCode = StatusCode.Conflict}
                : new Response {StatusCode = StatusCode.Ok};
        }
    }
}