using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Manager;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Users
{
    /// <summary>
    /// Gets the stats of a specific user
    /// </summary>
    public class GetStatsCommand : ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;

        /// <summary>
        /// Sets user manager
        /// </summary>
        /// <param name="userManager">the usermanager</param>
        public GetStatsCommand(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            Stats stats;
            try
            {
                stats = _userManager.GetUserStats(User.Username);
            }
            catch (UserNotFoundException)
            {
                stats = null;
            }

            var response = new Response();
            if (stats == null)
            {
                response.StatusCode = StatusCode.NotFound;
            }
            else
            {
                response.Payload = JsonConvert.SerializeObject(stats);
                response.StatusCode = StatusCode.Ok;
            }

            return response;
        }
    }
}