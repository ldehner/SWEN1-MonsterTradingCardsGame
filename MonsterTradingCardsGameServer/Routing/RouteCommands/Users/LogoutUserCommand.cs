using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Manager;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Users
{
    /// <summary>
    /// Logs an user out
    /// </summary>
    public class LogoutUserCommand : ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;

        /// <summary>
        /// Sets user manager
        /// </summary>
        /// <param name="userManager">user manager</param>
        public LogoutUserCommand(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            try
            {
                return _userManager.LogoutUser(User.Token)
                    ? new Response {StatusCode = StatusCode.Ok}
                    : new Response {StatusCode = StatusCode.Conflict};
            }
            catch (UserNotFoundException)
            {
                return new Response() {StatusCode = StatusCode.Unauthorized};
            }
        }
    }
}