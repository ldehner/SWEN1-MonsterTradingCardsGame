using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Trades
{
    /// <summary>
    /// Deletes a trade
    /// </summary>
    public class DeleteTradeCommand : ProtectedRouteCommand
    {
        private readonly string _tradeId;
        private readonly IUserManager _userManager;

        /// <summary>
        /// Sets user manager and trade id
        /// </summary>
        /// <param name="userManager">the user manager</param>
        /// <param name="tradeId">the id of the trade the user wants to delete</param>
        public DeleteTradeCommand(IUserManager userManager, string tradeId)
        {
            _userManager = userManager;
            _tradeId = tradeId;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            return _userManager.DeleteTrade(User.Username, _tradeId)
                ? new Response {StatusCode = StatusCode.Ok}
                : new Response {StatusCode = StatusCode.Unauthorized};
        }
    }
}