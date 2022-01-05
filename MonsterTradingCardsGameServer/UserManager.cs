using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.DAL;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer
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
            var user = UserRepository.GetUserByCredentials(credentials.Username, credentials.Password);
            return user ?? throw new UserNotFoundException();
        }

        public UserData GetUserData(string username)
        {
            var data = UserRepository.GetUserDataByUsername(username);
            return data ?? throw new UserNotFoundException();
        }

        public void RegisterUser(Credentials credentials)
        {
            var user = new User(credentials.Username, new Stats(0, 0), new UserData(20, "Tolle Bio"),
                new Stack(new List<Card>()), new Deck(new List<Card>()));
            if (UserRepository.InsertUser(user, credentials.Password) == false)
            {
                throw new DuplicateUserException();
            }
        }
    }
}