using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Manager
{
    /// <summary>
    ///     Card manager interface
    /// </summary>
    public interface ICardManager
    {
        /// <summary>
        ///     Gets the stack of a user
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>users stack</returns>
        public Stack GetStack(string username);

        /// <summary>
        ///     Gets the deck of a user
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>users deck</returns>
        public Deck GetDeck(string username);

        /// <summary>
        ///     Gets the deck of a user and returns it in plain text
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>users deck</returns>
        public string GetPlainDeck(string username);

        /// <summary>
        ///     sets users deck
        /// </summary>
        /// <param name="username">users deck</param>
        /// <param name="ids">list of ids</param>
        /// <returns>if query was successful</returns>
        public bool SetDeck(string username, List<string> ids);

        /// <summary>
        ///     sets stack of user
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>if query was successful</returns>
        public bool SetStack(User user);
    }
}