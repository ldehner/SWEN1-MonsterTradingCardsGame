using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Users
{
    public class EditBioCommand : ProtectedRouteCommand
    {
        private readonly UserData _userData;
        private readonly IUserManager _userManager;
        private readonly string _username;

        public EditBioCommand(IUserManager userManager, string username, UserData userData)
        {
            _userManager = userManager;
            _username = username;
            _userData = userData;
        }

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