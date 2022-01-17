using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Manager;
using MonsterTradingCardsGameServer.Trades;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Trades
{
    /// <summary>
    /// Creates a new trade
    /// </summary>
    public class CreateTradeCommand : ProtectedRouteCommand
    {
        private readonly UserRequestTrade _userRequestTrade;
        private readonly ITradeManager _tradeManager;

        /// <summary>
        /// Sets user manager and users trade
        /// </summary>
        /// <param name="tradeManager">the trade manager</param>
        /// <param name="userRequestTrade">the trade of the user</param>
        public CreateTradeCommand(ITradeManager tradeManager, UserRequestTrade userRequestTrade)
        {
            _tradeManager = tradeManager;
            _userRequestTrade = userRequestTrade;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            return _tradeManager.CreateTrade(User.Username, _userRequestTrade)
                ? new Response {StatusCode = StatusCode.Created}
                : new Response {StatusCode = StatusCode.Conflict};
        }
    }
}