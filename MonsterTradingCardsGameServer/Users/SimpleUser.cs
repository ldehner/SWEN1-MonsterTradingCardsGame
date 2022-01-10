using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.Users
{
    public class SimpleUser:ISimpleUser
    {
        public sealed override string Username { get; set; }
        public sealed override Stats Stats { get; set; }

        public SimpleUser(string username, Stats stats)
        {
            Username = username;
            Stats = stats;
        }
    }
}