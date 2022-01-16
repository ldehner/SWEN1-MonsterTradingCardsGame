using System;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Trades;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Trades
{
    /// <summary>
    /// Accepts a trade
    /// </summary>
    public class AcceptTradeCommand : ProtectedRouteCommand
    {
        private readonly string _tradeId;
        private readonly string _cardId;
        private readonly IUserManager _userManager;

        /// <summary>
        /// Sets user manager, trade id and card id
        /// </summary>
        /// <param name="userManager">the user manager</param>
        /// <param name="tradeId">the id of the trade, the user wants to accept</param>
        /// <param name="cardId">the id of the card, the user wants to trade in return</param>
        public AcceptTradeCommand(IUserManager userManager, string tradeId, string cardId)
        {
            _userManager = userManager;
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
                return _userManager.AcceptTrade(User.Username, _tradeId, _cardId)
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