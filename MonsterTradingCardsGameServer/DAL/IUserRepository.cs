using System.Collections.Generic;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.DAL
{
    public interface IUserRepository
    {
        User GetUserByCredentials(string username, string password);

        User GetUserByAuthToken(string authToken);

        UserData GetUserDataByUsername(string username);

        bool InsertUser(User user, string password);
    }
}