namespace MonsterTradingCardsGameServer.Users
{
    public abstract class ISimpleUser
    {
        public abstract string Username { get; set; }
        public abstract Stats Stats { get; set; }
    }
}