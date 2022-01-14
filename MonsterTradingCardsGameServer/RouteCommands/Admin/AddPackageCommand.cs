using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.RouteCommands.Admin
{
    public class AddPackageCommand:ProtectedRouteCommand
    {
        private IUserManager _userManager;
        private User _currentUser;
        private List<UserRequestCard> _package;

        public AddPackageCommand(IUserManager userManager, User currentUser, List<UserRequestCard> package)
        {
            _userManager = userManager;
            _currentUser = currentUser;
            _package = package;
        }
        public override Response Execute()
        {
            var response = new Response
            {
                StatusCode = !_currentUser.Username.Equals("admin") ? StatusCode.Unauthorized :
                    _userManager.AddPackage(_currentUser.Username, _package) ? StatusCode.Created : StatusCode.Conflict
            };
            return response;
        }
    }
}