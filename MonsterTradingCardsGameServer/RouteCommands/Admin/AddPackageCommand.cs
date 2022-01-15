using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.RouteCommands.Admin
{
    /// <summary>
    /// Adds Package
    /// </summary>
    public class AddPackageCommand : ProtectedRouteCommand
    {
        private readonly User _currentUser;
        private readonly List<UserRequestCard> _package;
        private readonly IUserManager _userManager;
        
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
                    _userManager.AddPackage(_package) ? StatusCode.Created : StatusCode.Conflict
            };
            return response;
        }
    }
}