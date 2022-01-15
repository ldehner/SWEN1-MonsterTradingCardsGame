namespace MonsterTradingCardsGameServer.Users
{
    /// <summary>
    /// Simple user class so that unneccessary data
    /// isn't exposed
    /// </summary>
    public class SimpleUser : ISimpleUser
    {
        /// <summary>
        /// Sets all attributes
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="stats">users stats</param>
        public SimpleUser(string username, Stats stats)
        {
            Username = username;
            Stats = stats;
        }

        public sealed override string Username { get; set; }
        public sealed override Stats Stats { get; set; }
    }
}