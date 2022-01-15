namespace MonsterTradingCardsGameServer.Users
{
    public class Score
    {
        public Score(string username, Stats stats)
        {
            Username = username;
            Stats = stats;
        }
        
        public string Username { get; set; }
        public Stats Stats { get; set; }
    }
}