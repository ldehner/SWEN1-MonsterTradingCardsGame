namespace MonsterTradingCardsGameServer.Users
{
    public class Score
    {
        public string Username { get; set; }
        public Stats Stats { get; set; }

        public Score(string username, Stats stats)
        {
            Username = username;
            Stats = stats;
        }
    }
}