using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.DAL
{
    /// <summary>
    /// User Repository, inherits all methots for user 
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets user out of db with credentials and compares password
        /// </summary>
        /// <param name="username">wanted user</param>
        /// <param name="password">provided password</param>
        /// <returns></returns>
        User GetUserByCredentials(string username, string password);

        /// <summary>
        /// Gets an user by his auth token if he has signed in
        /// </summary>
        /// <param name="authToken">auth token of the user</param>
        /// <returns>the user</returns>
        User GetUserByAuthToken(string authToken);

        /// <summary>
        /// Gets the user by his username
        /// </summary>
        /// <param name="username">username of user</param>
        /// <returns>the user</returns>
        User GetUserByUsername(string username);

        /// <summary>
        /// Updates bio, name and icon of the user
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="userData">new user data</param>
        /// <returns>if query was successful</returns>
        bool UpdateUserData(string username, UserData userData);

        /// <summary>
        /// Inserts a user into the database
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="password">passsword of the user</param>
        /// <returns>if query was successful</returns>
        bool InsertUser(User user, string password);

        /// <summary>
        /// Gets all users scores
        /// </summary>
        /// <returns>Returns a list of Scores if successful</returns>
        public List<Score> GetScoreBoard();

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
        /// Adds a new package into the db
        /// </summary>
        /// <param name="package">universal card list</param>
        /// <param name="id">uid of the package</param>
        /// <returns>if query was successful</returns>
        public bool AddPackage(List<UniversalCard> package, Guid id);

        /// <summary>
        /// Adds an quicred package into the stack of the user and updates the number of coins
        /// </summary>
        /// <param name="username">wanted user</param>
        /// <param name="coins">users number of coins</param>
        /// <param name="stack">users stack</param>
        /// <returns>if query was successful</returns>
        public bool AquirePackage(string username, int coins, Stack stack);

        /// <summary>
        /// Creates a new trade offer
        /// </summary>
        /// <param name="username">traders username</param>
        /// <param name="card">card trader wants to trade</param>
        /// <param name="minDmg">min damage requirement</param>
        /// <param name="tradeId">trade uid</param>
        /// <param name="type">required card type</param>
        /// <returns>if query was successful</returns>
        public bool CreateTrade(string username, Card card, double minDmg, string tradeId, int type);

        /// <summary>
        /// Lists all trading offers
        /// </summary>
        /// <returns>all trading offers</returns>
        public List<TradingOffer> ListTrades();

        /// <summary>
        /// Gets a specific trade
        /// </summary>
        /// <param name="tradeId">trade id</param>
        /// <returns>the trade</returns>
        public Trade GetTrade(string tradeId);

        /// <summary>
        /// deletes a trade
        /// </summary>
        /// <param name="tradeId">id of the trade</param>
        /// <returns>if query was successful</returns>
        public bool DeleteTrade(string tradeId);

        /// <summary>
        /// deletes the offer, and updates both users stacks
        /// </summary>
        /// <param name="tradeId">id of the trade</param>
        /// <param name="seller">seller</param>
        /// <param name="buyer">buyer</param>
        /// <returns>if query was successful</returns>
        public bool AcceptTrade(string tradeId, User seller, User buyer);

        /// <summary>
        /// Sets the stack of an user
        /// </summary>
        /// <param name="user">wanted user</param>
        /// <returns>if query was successful</returns>
        public bool SetStack(User user);

        /// <summary>
        /// deletes user out of active users
        /// </summary>
        /// <param name="token">token of the user</param>
        /// <returns>if query was successful</returns>
        public bool LogoutUser(string token);
    }
}