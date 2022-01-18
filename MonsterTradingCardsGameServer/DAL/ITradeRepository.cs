using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Trades;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.DAL
{
    /// <summary>
    ///     Interface for trade repository
    /// </summary>
    public interface ITradeRepository
    {
        /// <summary>
        ///     Creates a new trade offer
        /// </summary>
        /// <param name="username">traders username</param>
        /// <param name="card">card trader wants to trade</param>
        /// <param name="minDmg">min damage requirement</param>
        /// <param name="tradeId">trade uid</param>
        /// <param name="type">required card type</param>
        /// <returns>if query was successful</returns>
        public bool CreateTrade(string username, Card card, double minDmg, string tradeId, int type);

        /// <summary>
        ///     Lists all trading offers
        /// </summary>
        /// <returns>all trading offers</returns>
        public List<ReadableTrade> ListTrades();

        /// <summary>
        ///     Gets a specific trade
        /// </summary>
        /// <param name="tradeId">trade id</param>
        /// <returns>the trade</returns>
        public UniversalTrade GetTrade(string tradeId);

        /// <summary>
        ///     deletes a trade
        /// </summary>
        /// <param name="tradeId">id of the trade</param>
        /// <returns>if query was successful</returns>
        public bool DeleteTrade(string tradeId);

        /// <summary>
        ///     deletes the offer, and updates both users stacks
        /// </summary>
        /// <param name="tradeId">id of the trade</param>
        /// <param name="seller">seller</param>
        /// <param name="buyer">buyer</param>
        /// <returns>if query was successful</returns>
        public bool AcceptTrade(string tradeId, User seller, User buyer);
    }
}