namespace MonsterTradingCardsGameServer.Users
{
    /// <summary>
    ///     Score for the scorebord
    ///     with username and stats
    /// </summary>
    public class Score
    {
        /// <summary>
        ///     Sets the attributes
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="stats">users stats</param>
        public Score(string username, Stats stats)
        {
            Username = username;
            Stats = stats;
        }

        public string Username { get; set; }
        public Stats Stats { get; set; }
    }
}