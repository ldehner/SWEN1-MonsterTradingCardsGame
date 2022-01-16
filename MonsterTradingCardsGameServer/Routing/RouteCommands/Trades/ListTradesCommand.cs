using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Trades
{
    /// <summary>
    /// Lists all available trades
    /// </summary>
    public class ListTradesCommand : ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;

        /// <summary>
        /// Sets user manager
        /// </summary>
        /// <param name="userManager">the user manager</param>
        public ListTradesCommand(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            var response = new Response();
            var result = _userManager.ListTrades();
            response.Payload = JsonConvert.SerializeObject(result);
            response.StatusCode = StatusCode.Ok;
            return response;
        }
    }
}