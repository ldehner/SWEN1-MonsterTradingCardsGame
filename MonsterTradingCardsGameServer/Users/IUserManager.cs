using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.Users
{
    public interface IUserManager
    {
        User LoginUser(Credentials credentials);
        void RegisterUser(Credentials credentials);
        UserData GetUserData(string username);
        void EditUserData(string username, UserData userData);
        public User GetUser(string username);
        Stats GetUserStats(string username);
        public List<Score> GetScores();

        public Stack GetStack(string username);
        
        public Deck GetDeck(string username);

        public bool SetDeck(string username, List<string> ids);

        public bool AddPackage(string username, List<UserRequestCard> package);

        public bool AquirePackage(string username);
    }
}