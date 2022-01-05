using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer
{
    public interface IUserManager
    {
        User LoginUser(Credentials credentials);
        void RegisterUser(Credentials credentials);
        public UserData GetUserData(string username);
    }
}