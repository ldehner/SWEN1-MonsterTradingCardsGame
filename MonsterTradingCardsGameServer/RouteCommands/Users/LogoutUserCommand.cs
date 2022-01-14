using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.RouteCommands.Users
{
    public class LogoutUserCommand:ProtectedRouteCommand
    {
        private IUserManager _userManager;
        private User _currentUser;
        
        public LogoutUserCommand(IUserManager userManager, User currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }
        public override Response Execute()
        {
            return _userManager.LogoutUser(_currentUser.Token)
                ? new Response() {StatusCode = StatusCode.Ok}
                : new Response() {StatusCode = StatusCode.Conflict};
        }
    }
}