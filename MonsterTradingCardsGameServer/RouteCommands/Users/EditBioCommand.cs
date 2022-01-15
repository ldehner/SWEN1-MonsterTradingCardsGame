using System;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.RouteCommands.Users
{
    public class EditBioCommand : ProtectedRouteCommand
    {
        private readonly User _currentUser;
        private readonly UserData _userData;
        private readonly IUserManager _userManager;
        private readonly string _username;

        public EditBioCommand(IUserManager userManager, string username, User currentUser, UserData userData)
        {
            Console.WriteLine("Username: " + username);
            _userManager = userManager;
            _username = username;
            _currentUser = currentUser;
            _userData = userData;
            Console.WriteLine("Current user " + _currentUser.Username);
        }

        public override Response Execute()
        {
            var response = new Response();
            if (!_username.Equals(_currentUser.Username))
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