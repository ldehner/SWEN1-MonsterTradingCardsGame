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

        public List<Card> GetStack(string username);
        
        public List<Card> GetDeck(string username);

        public bool SetDeck(string username, List<string> ids);
    }
}