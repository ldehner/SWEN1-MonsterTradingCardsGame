namespace MonsterTradingCardsGameServer.Users
{
    public class SimpleUser : ISimpleUser
    {
        public SimpleUser(string username, Stats stats)
        {
            Username = username;
            Stats = stats;
        }

        public sealed override string Username { get; set; }
        public sealed override Stats Stats { get; set; }
    }
}