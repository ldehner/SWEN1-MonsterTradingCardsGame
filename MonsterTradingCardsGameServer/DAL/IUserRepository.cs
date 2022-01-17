using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Trades;
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
        /// deletes user out of active users
        /// </summary>
        /// <param name="token">token of the user</param>
        /// <returns>if query was successful</returns>
        public bool LogoutUser(string token);
    }
}