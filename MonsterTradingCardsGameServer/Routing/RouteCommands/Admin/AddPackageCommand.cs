using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Admin
{
    /// <summary>
    /// Adds Package
    /// </summary>
    public class AddPackageCommand : ProtectedRouteCommand
    {
        private readonly List<UserRequestCard> _package;
        private readonly IUserManager _userManager;
        
        /// <summary>
        /// Sets user manager and package
        /// </summary>
        /// <param name="userManager">the user manager</param>
        /// <param name="package">the package to add</param>
        public AddPackageCommand(IUserManager userManager, List<UserRequestCard> package)
        {
            _userManager = userManager;
            _package = package;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            var response = new Response
            {
                StatusCode = !User.Username.Equals("admin") ? StatusCode.Unauthorized :
                    _userManager.AddPackage(_package) ? StatusCode.Created : StatusCode.Conflict
            };
            return response;
        }
    }
}