using Microsoft.VisualBasic;

namespace MonsterTradingCardsGameServer.Users
{
    public class Stats
    {
        public int Wins { get; set; }
        public int Losses { get; set; }

        public Stats(int wins, int losses)
        {
            Wins = wins;
            Losses = losses;
        }
    }
}