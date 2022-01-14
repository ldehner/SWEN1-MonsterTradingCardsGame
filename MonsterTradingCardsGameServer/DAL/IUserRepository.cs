using System;
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
        
        public Stack GetStack(string username);
        
        public Deck GetDeck(string username);
        
        public bool SetDeck (string username, Deck deck);

        public bool AddPackage(string username, List<UniversalCard> package, Guid id);

        public bool AquirePackage(string username, int coins, Stack stack);

        public bool CreateTrade(string username, Card card, double minDmg, string tradeId);
    }
}