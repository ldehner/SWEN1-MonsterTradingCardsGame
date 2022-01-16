using System;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Cards
{
    /// <summary>
    /// Trys to aquire a package
    /// </summary>
    public class AquirePackageCommand : ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;

        /// <summary>
        /// Sets the user manager
        /// </summary>
        /// <param name="userManager">the user manager</param>
        public AquirePackageCommand(IUserManager userManager)
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
                return !_userManager.AquirePackage(User.Username)
                    ? new Response {StatusCode = StatusCode.Conflict}
                    : new Response {StatusCode = StatusCode.Ok};
            }
            catch (Exception)
            {
                return new Response() {StatusCode = StatusCode.Conflict};
            }
        }
    }
}