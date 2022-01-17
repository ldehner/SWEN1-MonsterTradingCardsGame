using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.DAL
{
    /// <summary>
    /// Card repository interface
    /// </summary>
    public interface ICardRepository
    {
        /// <summary>
        /// Gets the stack of the user
        /// </summary>
        /// <param name="username">wanted user</param>
        /// <returns>the stack</returns>
        public Stack GetStack(string username);

        /// <summary>
        /// Gets the deck of the user
        /// </summary>
        /// <param name="username">wanted user</param>
        /// <returns>deck of the user</returns>
        public Deck GetDeck(string username);

        /// <summary>
        /// sets the deck of the user
        /// </summary>
        /// <param name="username">wanted user</param>
        /// <param name="deck">new deck</param>
        /// <returns>if query was successful</returns>
        public bool SetDeck(string username, Deck deck);
        
        /// <summary>
        /// Sets the stack of an user
        /// </summary>
        /// <param name="user">wanted user</param>
        /// <returns>if query was successful</returns>
        public bool SetStack(User user);

        /// <summary>
        /// Updates users stack
        /// </summary>
        /// <param name="stack">new stack</param>
        /// <param name="coins">users coins</param>
        /// <param name="minusCoins">how many coins should be deducted</param>
        /// <param name="username">users username</param>
        /// <returns>if query was successful</returns>
        public bool UpdateStack(List<UniversalCard> stack, int coins, int minusCoins, string username);
    }
}