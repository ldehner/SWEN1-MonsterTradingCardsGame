using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Core.Routing;
using MonsterTradingCardsGameServer.Manager;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Users
{
    /// <summary>
    ///     Logs an user in
    /// </summary>
    public class LoginCommand : IRouteCommand
    {
        private readonly IUserManager _userManager;

        /// <summary>
        ///     Sets user manager and credentials
        /// </summary>
        /// <param name="userManager">the user manager</param>
        /// <param name="credentials">the credentials of the user</param>
        public LoginCommand(IUserManager userManager, Credentials credentials)
        {
            Credentials = credentials;
            _userManager = userManager;
        }

        private Credentials Credentials { get; }

        /// <summary>
        ///     Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public Response Execute()
        {
            User user;
            try
            {
                user = _userManager.LoginUser(Credentials);
            }
            catch (UserNotFoundException)
            {
                user = null;
            }

            var response = new Response();
            if (user == null)
            {
                response.StatusCode = StatusCode.Unauthorized;
            }
            else
            {
                response.StatusCode = StatusCode.Ok;
                response.Payload = user.Token;
            }

            return response;
        }
    }
}