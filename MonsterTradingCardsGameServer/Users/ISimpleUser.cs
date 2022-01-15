namespace MonsterTradingCardsGameServer.Users
{
    /// <summary>
    /// Simple user
    /// </summary>
    public abstract class ISimpleUser
    {
        public abstract string Username { get; set; }
        public abstract Stats Stats { get; set; }
    }
}