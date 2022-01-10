using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Users;
using Npgsql;

namespace MonsterTradingCardsGameServer.DAL
{
    public interface IUserRepository
    {
        User GetUserByCredentials(string username, string password);

        User GetUserByAuthToken(string authToken);

        User GetUserByUsername(string username);

        User GetUser(NpgsqlDataReader reader);

        bool UpdateUserData(string username, UserData userData);

        bool InsertUser(User user, string password);

        public List<Score> GetScoreBoard();
        
        public List<Card> GetStack(string username);
        
        public List<Card> GetDeck(string username);
        
        public bool SetDeck (string username, List<Card> cards);
    }
}