using System;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Manager;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Cards
{
    /// <summary>
    /// Trys to aquire a package
    /// </summary>
    public class AquirePackageCommand : ProtectedRouteCommand
    {
        private readonly IPackageManager _packageManager;

        /// <summary>
        /// Sets the user manager
        /// </summary>
        /// <param name="packageManager">the package manager</param>
        public AquirePackageCommand(IPackageManager packageManager)
        {
            _packageManager = packageManager;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            try
            {
                return !_packageManager.AquirePackage(User)
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