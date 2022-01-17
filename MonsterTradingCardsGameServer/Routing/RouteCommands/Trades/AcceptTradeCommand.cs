using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Manager;
using MonsterTradingCardsGameServer.Trades;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Trades
{
    /// <summary>
    /// Accepts a trade
    /// </summary>
    public class AcceptTradeCommand : ProtectedRouteCommand
    {
        private readonly string _tradeId;
        private readonly string _cardId;
        private readonly ITradeManager _tradeManager;

        /// <summary>
        /// Sets user manager, trade id and card id
        /// </summary>
        /// <param name="tradeManager">the trade manager</param>
        /// <param name="tradeId">the id of the trade, the user wants to accept</param>
        /// <param name="cardId">the id of the card, the user wants to trade in return</param>
        public AcceptTradeCommand(ITradeManager tradeManager, string tradeId, string cardId)
        {
            _tradeManager = tradeManager;
            _tradeId = tradeId;
            _cardId = cardId;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            try
            {
                return _tradeManager.AcceptTrade(User.Username, _tradeId, _cardId)
                    ? new Response {StatusCode = StatusCode.Ok}
                    : new Response {StatusCode = StatusCode.Conflict};
            }
            catch (NoSuchTradeException)
            {
                return new Response() {StatusCode = StatusCode.NotFound};
            }
            
        }
    }
}