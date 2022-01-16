using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Users
{
    public class LogoutUserCommand : ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;

        public LogoutUserCommand(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public override Response Execute()
        {
            return _userManager.LogoutUser(User.Token)
                ? new Response {StatusCode = StatusCode.Ok}
                : new Response {StatusCode = StatusCode.Conflict};
        }
    }
}