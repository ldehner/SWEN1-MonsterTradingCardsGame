using System;
using Microsoft.VisualBasic;
using MonsterTradingCardsGameServer.Battles;

namespace MonsterTradingCardsGameServer.Users
{
    public class Stats
    {
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Elo { get; set; }

        public Stats(int wins, int losses)
        {
            Wins = wins;
            Losses = losses;
            Elo = 100;
        }
    }
}