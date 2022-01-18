namespace MonsterTradingCardsGameServer.Users
{
    /// <summary>
    ///     Stats of the user
    /// </summary>
    public class Stats
    {
        /// <summary>
        ///     sets all attributes
        /// </summary>
        /// <param name="wins">users wins</param>
        /// <param name="losses">users losses</param>
        public Stats(int wins, int losses)
        {
            Wins = wins;
            Losses = losses;
            Elo = 100;
        }

        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Elo { get; set; }
    }
}