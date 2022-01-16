using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Trades;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Trades
{
    /// <summary>
    /// Creates a new trade
    /// </summary>
    public class CreateTradeCommand : ProtectedRouteCommand
    {
        private readonly UserRequestTrade _userRequestTrade;
        private readonly IUserManager _userManager;

        /// <summary>
        /// Sets user manager and users trade
        /// </summary>
        /// <param name="userManager">the user manager</param>
        /// <param name="userRequestTrade">the trade of the user</param>
        public CreateTradeCommand(IUserManager userManager, UserRequestTrade userRequestTrade)
        {
            _userManager = userManager;
            _userRequestTrade = userRequestTrade;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            return _userManager.CreateTrade(User.Username, _userRequestTrade)
                ? new Response {StatusCode = StatusCode.Created}
                : new Response {StatusCode = StatusCode.Conflict};
        }
    }
}