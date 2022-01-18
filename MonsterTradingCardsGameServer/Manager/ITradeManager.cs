using System.Collections.Generic;
using MonsterTradingCardsGameServer.Trades;

namespace MonsterTradingCardsGameServer.Manager
{
    /// <summary>
    ///     trade manager interface
    /// </summary>
    public interface ITradeManager
    {
        /// <summary>
        ///     creates a new trading offer
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="userRequestTrade">trading deal</param>
        /// <returns>if query was successful</returns>
        public bool CreateTrade(string username, UserRequestTrade userRequestTrade);

        /// <summary>
        ///     lists all available trades
        /// </summary>
        /// <returns>all trades</returns>
        public List<ReadableTrade> ListTrades();

        /// <summary>
        ///     Accepts a trade and trades cards
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="tradeId">id of the trade</param>
        /// <param name="cardId">id of the card to trade</param>
        /// <returns>if query was successful</returns>
        public bool AcceptTrade(string username, string tradeId, string cardId);

        /// <summary>
        ///     Deletes a specific trade
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="tradeId">id of the trade</param>
        /// <returns>if query was successful</returns>
        public bool DeleteTrade(string username, string tradeId);
    }
}