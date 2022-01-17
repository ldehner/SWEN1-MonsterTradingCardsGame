using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.DAL;
using MonsterTradingCardsGameServer.Trades;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Manager
{
    /// <summary>
    /// UserManager
    /// </summary>
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Sets the repository
        /// </summary>
        /// <param name="userRepository"></param>
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            ActiveUsers = new Dictionary<string, User>();
        }

        private Dictionary<string, User> ActiveUsers { get; set; }

        /// <summary>
        /// Loggs in User
        /// </summary>
        /// <param name="credentials">users credentials</param>
        /// <returns>user</returns>
        public User LoginUser(Credentials credentials)
        {
            return _userRepository.GetUserByCredentials(credentials.Username, credentials.Password) ??
                   throw new UserNotFoundException();
        }

        /// <summary>
        /// gets user
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>user</returns>
        public User GetUser(string username)
        {
            return _userRepository.GetUserByUsername(username) ?? throw new UserNotFoundException();
        }

        /// <summary>
        /// gets users data
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>userdata of user</returns>
        public UserData GetUserData(string username)
        {
            return _userRepository.GetUserByUsername(username).UserData ?? throw new UserNotFoundException();
        }

        /// <summary>
        /// changes users data
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="userData">new userdata</param>
        public void EditUserData(string username, UserData userData)
        {
            if (!_userRepository.UpdateUserData(username, userData)) throw new UserNotFoundException();
        }

        /// <summary>
        /// Lists all scores
        /// </summary>
        /// <returns>the score list</returns>
        public List<Score> GetScores()
        {
            return _userRepository.GetScoreBoard();
        }

        /// <summary>
        /// gets users stats
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>users stats</returns>
        public Stats GetUserStats(string username)
        {
            return _userRepository.GetUserByUsername(username).Stats ?? throw new UserNotFoundException();
        }

        /// <summary>
        /// registers user
        /// </summary>
        /// <param name="credentials">users credentials</param>
        public void RegisterUser(Credentials credentials)
        {
            var user = new User(credentials.Username, new Stats(0, 0),
                new UserData(credentials.Username, "Hey, ich bin neu!", ":)"),
                new Stack(new List<Card>()), new Deck(new List<Card>()), 20);
            if (!_userRepository.InsertUser(user, credentials.Password)) throw new DuplicateUserException();
        }

        /// <summary>
        /// logs out user
        /// </summary>
        /// <param name="token">users token</param>
        /// <returns>if query was successful</returns>
        public bool LogoutUser(string token)
        {
            return _userRepository.LogoutUser(token);
        }
    }
}