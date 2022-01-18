using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Manager;
using MonsterTradingCardsGameServer.Trades;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Trades
{
    /// <summary>
    ///     Deletes a trade
    /// </summary>
    public class DeleteTradeCommand : ProtectedRouteCommand
    {
        private readonly string _tradeId;
        private readonly ITradeManager _tradeManager;

        /// <summary>
        ///     Sets user manager and trade id
        /// </summary>
        /// <param name="tradeManager">the trade manager</param>
        /// <param name="tradeId">the id of the trade the user wants to delete</param>
        public DeleteTradeCommand(ITradeManager tradeManager, string tradeId)
        {
            _tradeManager = tradeManager;
            _tradeId = tradeId;
        }

        /// <summary>
        ///     Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            try
            {
                return _tradeManager.DeleteTrade(User.Username, _tradeId)
                    ? new Response {StatusCode = StatusCode.Ok}
                    : new Response {StatusCode = StatusCode.Unauthorized};
            }
            catch (NoSuchTradeException)
            {
                return new Response {StatusCode = StatusCode.NotFound};
            }
        }
    }
}