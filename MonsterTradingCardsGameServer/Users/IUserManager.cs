using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.Users
{
    /// <summary>
    /// Interface for User Manager
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Loggs in User
        /// </summary>
        /// <param name="credentials">users credentials</param>
        /// <returns>user</returns>
        User LoginUser(Credentials credentials);
        
        /// <summary>
        /// registers user
        /// </summary>
        /// <param name="credentials">users credentials</param>
        void RegisterUser(Credentials credentials);
        
        /// <summary>
        /// gets users data
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>userdata of user</returns>
        UserData GetUserData(string username);
        
        /// <summary>
        /// changes users data
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="userData">new userdata</param>
        void EditUserData(string username, UserData userData);
        
        /// <summary>
        /// gets users stats
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>users stats</returns>
        Stats GetUserStats(string username);
        
        /// <summary>
        /// Lists all scores
        /// </summary>
        /// <returns>the score list</returns>
        public List<Score> GetScores();
        
        /// <summary>
        /// gets user
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>user</returns>
        public User GetUser(string username);
        
        /// <summary>
        /// Gets the stack of a user
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>users stack</returns>
        public Stack GetStack(string username);

        /// <summary>
        /// Gets the deck of a user
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>users deck</returns>
        public Deck GetDeck(string username);

        /// <summary>
        /// sets users deck
        /// </summary>
        /// <param name="username">users deck</param>
        /// <param name="ids">list of ids</param>
        /// <returns>if query was successful</returns>
        public bool SetDeck(string username, List<string> ids);

        /// <summary>
        /// adds an package
        /// </summary>
        /// <param name="package">list of cards</param>
        /// <returns>if query was successful</returns>
        public bool AddPackage(List<UserRequestCard> package);

        /// <summary>
        /// aquires an package
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>if query was successful</returns>
        public bool AquirePackage(string username);

        /// <summary>
        /// creates a new trading offer
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="tradingDeal">trading deal</param>
        /// <returns>if query was successful</returns>
        public bool CreateTrade(string username, TradingDeal tradingDeal);

        /// <summary>
        /// lists all available trades
        /// </summary>
        /// <returns>all trades</returns>
        public List<TradingOffer> ListTrades();

        /// <summary>
        /// Accepts a trade and trades cards
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="tradeId">id of the trade</param>
        /// <param name="cardId">id of the card to trade</param>
        /// <returns>if query was successful</returns>
        public bool AcceptTrade(string username, string tradeId, string cardId);

        /// <summary>
        /// Deletes a specific trade
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="tradeId">id of the trade</param>
        /// <returns>if query was successful</returns>
        public bool DeleteTrade(string username, string tradeId);

        /// <summary>
        /// logs out user
        /// </summary>
        /// <param name="token">users token</param>
        /// <returns>if query was successful</returns>
        public bool LogoutUser(string token);
    }
}