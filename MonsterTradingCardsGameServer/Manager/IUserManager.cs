using System.Collections.Generic;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Manager
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
        /// logs out user
        /// </summary>
        /// <param name="token">users token</param>
        /// <returns>if query was successful</returns>
        public bool LogoutUser(string token);
    }
}