using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Core.Routing;
using MonsterTradingCardsGameServer.Manager;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Users
{
    /// <summary>
    ///     Registers an user
    /// </summary>
    public class RegisterCommand : IRouteCommand
    {
        private readonly IUserManager _userManager;

        /// <summary>
        ///     Sets user manager and credentials
        /// </summary>
        /// <param name="userManager">the user manager</param>
        /// <param name="credentials">the users password and username</param>
        public RegisterCommand(IUserManager userManager, Credentials credentials)
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
            var response = new Response();
            try
            {
                _userManager.RegisterUser(Credentials);
                response.StatusCode = StatusCode.Created;
            }
            catch (DuplicateUserException)
            {
                response.StatusCode = StatusCode.Conflict;
            }

            return response;
        }
    }
}