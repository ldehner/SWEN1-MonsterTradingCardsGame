using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.DAL;
using MonsterTradingCardsGameServer.Trades;

namespace MonsterTradingCardsGameServer.Manager
{
    /// <summary>
    ///     Manages trades
    /// </summary>
    public class TradeManager : ITradeManager
    {
        private readonly ICardManager _cardManager;

        private readonly ITradeRepository _tradeRepository;
        private readonly IUserManager _userManager;

        /// <summary>
        ///     Sets the repository
        /// </summary>
        /// <param name="tradeRepository"></param>
        /// <param name="userManager"></param>
        /// <param name="cardManager"></param>
        public TradeManager(ITradeRepository tradeRepository, IUserManager userManager, ICardManager cardManager)
        {
            _tradeRepository = tradeRepository;
            _userManager = userManager;
            _cardManager = cardManager;
        }

        /// <summary>
        ///     creates a new trading offer
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="userRequestTrade">trading deal</param>
        /// <returns>if query was successful</returns>
        public bool CreateTrade(string username, UserRequestTrade userRequestTrade)
        {
            var user = _userManager.GetUser(username);
            var cardInDeck = false;
            Card tradingCard = null;
            user.Stack.Cards.ForEach(card =>
            {
                if (card.Id.ToString().Equals(userRequestTrade.CardToTrade)) tradingCard = card;
            });
            user.Deck.Cards.ForEach(card =>
            {
                if (card.Id.ToString().Equals(userRequestTrade.CardToTrade)) cardInDeck = true;
            });
            user.Stack.Cards.Remove(tradingCard);
            return !cardInDeck && tradingCard is not null &&
                   _tradeRepository.CreateTrade(username, tradingCard, userRequestTrade.MinimumDamage,
                       userRequestTrade.Id,
                       userRequestTrade.Type.Equals("monster") ? 1 : 0) && _cardManager.SetStack(user);
        }

        /// <summary>
        ///     lists all available trades
        /// </summary>
        /// <returns>all trades</returns>
        public List<ReadableTrade> ListTrades()
        {
            return _tradeRepository.ListTrades();
        }

        /// <summary>
        ///     Accepts a trade and trades cards
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="tradeId">id of the trade</param>
        /// <param name="cardId">id of the card to trade</param>
        /// <returns>if query was successful</returns>
        public bool AcceptTrade(string username, string tradeId, string cardId)
        {
            var cardInDeck = false;
            Card tradingCard = null;
            var buyer = _userManager.GetUser(username);
            buyer.Stack.Cards.ForEach(card =>
            {
                if (card.Id.ToString().Equals(cardId)) tradingCard = card;
            });
            buyer.Deck.Cards.ForEach(card =>
            {
                if (card.Id.ToString().Equals(cardId)) cardInDeck = true;
            });
            if (tradingCard is null || cardInDeck) return false;
            var deal = _tradeRepository.GetTrade(tradeId);
            if (deal is null) throw new NoSuchTradeException();
            if (deal.Trader.Equals(username) || tradingCard.Damage < deal.RequiredDamage ||
                !tradingCard.GetCardType().Equals(deal.RequiredType)) return false;
            var seller = _userManager.GetUser(deal.Trader);
            seller.Stack.Cards.Add(tradingCard);
            buyer.Stack.Cards.Remove(tradingCard);
            buyer.Stack.Cards.Add(deal.Card);
            return _tradeRepository.AcceptTrade(tradeId, seller, buyer);
        }

        /// <summary>
        ///     Deletes a specific trade
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="tradeId">id of the trade</param>
        /// <returns>if query was successful</returns>
        public bool DeleteTrade(string username, string tradeId)
        {
            var trade = _tradeRepository.GetTrade(tradeId);
            if (trade is null) throw new NoSuchTradeException();
            var user = _userManager.GetUser(username);
            user.Stack.Cards.Add(trade.Card);
            return trade.Trader.Equals(username) && _cardManager.SetStack(user) &&
                   _tradeRepository.DeleteTrade(tradeId);
        }
    }
}