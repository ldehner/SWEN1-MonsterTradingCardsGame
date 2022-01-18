using System;
using System.Collections.Generic;
using System.Text;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.DAL;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Manager
{
    /// <summary>
    ///     manages cards
    /// </summary>
    public class CardManager : ICardManager
    {
        private readonly ICardRepository _cardRepository;

        /// <summary>
        ///     sets card repository
        /// </summary>
        /// <param name="cardRepository">card repository</param>
        public CardManager(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        /// <summary>
        ///     Gets the stack of a user
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>users stack</returns>
        public Stack GetStack(string username)
        {
            return _cardRepository.GetStack(username);
        }

        /// <summary>
        ///     Gets the deck of a user
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>users deck</returns>
        public Deck GetDeck(string username)
        {
            return _cardRepository.GetDeck(username);
        }

        /// <summary>
        ///     Gets the deck of a user and returns it in plain text
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>users deck</returns>
        public string GetPlainDeck(string username)
        {
            var deck = _cardRepository.GetDeck(username);
            if (deck is null) return null;
            if (deck.Cards.Count == 0) return "";
            var sb = new StringBuilder();
            var count = 1;
            deck.Cards.ForEach(card =>
            {
                if (count != 1) sb.Append(", ");
                sb.Append($"{card.GetCardName()} {card.Damage} Damage");
                count++;
            });
            return sb.ToString();
        }

        /// <summary>
        ///     sets users deck
        /// </summary>
        /// <param name="username">users deck</param>
        /// <param name="ids">list of ids</param>
        /// <returns>if query was successful</returns>
        public bool SetDeck(string username, List<string> ids)
        {
            var stack = GetStack(username);
            var newDeck = new List<Card>();
            ids.ForEach(cardId => stack.Cards.ForEach(card =>
            {
                var result = card.Id == Guid.Parse(cardId);
                if (result) newDeck.Add(card);
            }));
            return newDeck.Count == 4 && _cardRepository.SetDeck(username, new Deck(newDeck));
        }

        /// <summary>
        ///     sets stack of user
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>if query was successful</returns>
        public bool SetStack(User user)
        {
            return _cardRepository.SetStack(user);
        }
    }
}