namespace MonsterTradingCardsGameServer.Users
{
    public class Stats
    {
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