using System;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Manager;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Cards
{
    /// <summary>
    ///     Trys to aquire a package
    /// </summary>
    public class AquirePackageCommand : ProtectedRouteCommand
    {
        private readonly IPackageManager _packageManager;
        private readonly IUserManager _userManager;

        /// <summary>
        ///     Sets the user manager
        /// </summary>
        /// <param name="packageManager">the package manager</param>
        /// <param name="userManager">the user manager</param>
        public AquirePackageCommand(IPackageManager packageManager, IUserManager userManager)
        {
            _packageManager = packageManager;
            _userManager = userManager;
        }

        /// <summary>
        ///     Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            User dbUser;
            try
            {
                dbUser = _userManager.GetUser(User.Username);
            }
            catch (UserNotFoundException)
            {
                return new Response {StatusCode = StatusCode.Conflict};
            }

            try
            {
                return !_packageManager.AquirePackage(dbUser)
                    ? new Response {StatusCode = StatusCode.Conflict}
                    : new Response {StatusCode = StatusCode.Ok};
            }
            catch (Exception)
            {
                return new Response {StatusCode = StatusCode.Conflict};
            }
        }
    }
}