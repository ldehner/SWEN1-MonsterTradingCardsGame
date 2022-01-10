using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Battles
{
    public class BattleResult
    {
        public SimpleUser Winner { get; set; }
        public SimpleUser Loser { get; set; }
        public List<string> BattleLog { get; set; }
        public readonly Guid Guid;

        public BattleResult(SimpleUser winner, SimpleUser loser, List<string> battleLog)
        {
            Winner = winner;
            Loser = loser;
            BattleLog = battleLog;
            Guid = Guid.NewGuid();
        }
    }
}