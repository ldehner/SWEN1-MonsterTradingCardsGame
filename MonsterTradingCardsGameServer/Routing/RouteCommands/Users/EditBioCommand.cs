using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Manager;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Users
{
    /// <summary>
    /// Edits users bio
    /// </summary>
    public class EditBioCommand : ProtectedRouteCommand
    {
        private readonly UserData _userData;
        private readonly IUserManager _userManager;
        private readonly string _username;

        /// <summary>
        /// Sets user manager, username and users data
        /// </summary>
        /// <param name="userManager">the user manager</param>
        /// <param name="username">the user who's user data should be changed</param>
        /// <param name="userData">the new user data</param>
        public EditBioCommand(IUserManager userManager, string username, UserData userData)
        {
            _userManager = userManager;
            _username = username;
            _userData = userData;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            var response = new Response();
            if (!_username.Equals(User.Username))
            {
                response.StatusCode = StatusCode.Unauthorized;
                return response;
            }

            try
            {
                _userManager.EditUserData(_username, _userData);
                response.StatusCode = StatusCode.Ok;
            }
            catch (UserNotFoundException)
            {
                response.StatusCode = StatusCode.Conflict;
            }

            return response;
        }
    }
}