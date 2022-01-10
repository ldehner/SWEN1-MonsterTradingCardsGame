using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.DAL;
using Npgsql;

namespace MonsterTradingCardsGameServer.Users
{
    public class UserManager : IUserManager
    {
        public Dictionary<string, User> ActiveUsers { get; set; }
        public readonly IUserRepository UserRepository;

        public UserManager(IUserRepository userRepository)
        {
            UserRepository = userRepository;
            ActiveUsers = new Dictionary<string, User>();
        }

        public User LoginUser(Credentials credentials)
        {
            return UserRepository.GetUserByCredentials(credentials.Username, credentials.Password) ??
                   throw new UserNotFoundException();
        }

        public User GetUser(string username)
        {
            return UserRepository.GetUserByUsername(username) ?? throw new UserNotFoundException();
        }

        public UserData GetUserData(string username)
        {
            return UserRepository.GetUserByUsername(username).UserData ?? throw new UserNotFoundException();
        }


        public void EditUserData(string username, UserData userData)
        {
            if (!UserRepository.UpdateUserData(username, userData)) throw new UserNotFoundException();
        }

        public List<Score> GetScores()
        {
            return UserRepository.GetScoreBoard();
        }

        public List<Card> GetStack(string username)
        {
            return UserRepository.GetStack(username);
        }

        public List<Card> GetDeck(string username)
        {
            return UserRepository.GetDeck(username);
        }

        public bool SetDeck(string username, List<string> ids)
        {
            var stack = GetStack(username);
            var newDeck = new List<Card>();
            ids.ForEach(cardId => stack.ForEach(card =>
            {
                var result = card.Id == Guid.Parse(cardId);
                if (result) newDeck.Add(card);
            }));
            if (newDeck.Count != 4) return false;
            return UserRepository.SetDeck(username, newDeck);
        }

        public Stats GetUserStats(string username)
        {
            return UserRepository.GetUserByUsername(username).Stats ?? throw new UserNotFoundException();
        }

        public void RegisterUser(Credentials credentials)
        {
            var user = new User(credentials.Username, new Stats(0, 0),
                new UserData(credentials.Username, "Tolle Bio", ":-)"),
                new Stack(new List<Card>()), new Deck(new List<Card>()), 20);
            if (!UserRepository.InsertUser(user, credentials.Password)) throw new DuplicateUserException();
        }
    }
}