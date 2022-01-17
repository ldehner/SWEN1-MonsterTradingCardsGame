using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Manager;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Admin
{
    /// <summary>
    /// Adds Package
    /// </summary>
    public class AddPackageCommand : ProtectedRouteCommand
    {
        private readonly List<UserRequestCard> _package;
        private readonly IPackageManager _packageManager;

        /// <summary>
        /// Sets user manager and package
        /// </summary>
        /// <param name="packageManager"></param>
        /// <param name="package">the package to add</param>
        public AddPackageCommand(IPackageManager packageManager, List<UserRequestCard> package)
        {
            _packageManager = packageManager;
            _package = package;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            try
            {
                return new Response
                {
                    StatusCode = !User.Username.Equals("admin") ? StatusCode.Unauthorized :
                        _packageManager.AddPackage(_package) ? StatusCode.Created : StatusCode.Conflict
                };
            }
            catch (CardNotFoundException)
            {
                return new Response() {StatusCode = StatusCode.Conflict};
            }
        }
    }
}