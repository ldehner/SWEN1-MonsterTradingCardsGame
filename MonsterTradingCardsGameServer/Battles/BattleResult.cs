using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Battles
{
    /// <summary>
    ///     Battle result of two users battle
    /// </summary>
    public class BattleResult
    {
        public readonly Guid Guid;

        /// <summary>
        ///     Sets all attributes of a battleresult
        /// </summary>
        /// <param name="guid">the id of the battle</param>
        /// <param name="winner">the winner of the battle</param>
        /// <param name="loser">the loser of the battle</param>
        /// <param name="battleLog">the log of the battle</param>
        public BattleResult(string guid, SimpleUser winner, SimpleUser loser, List<string> battleLog)
        {
            Winner = winner;
            Loser = loser;
            BattleLog = battleLog;
            Guid = Guid.Parse(guid);
        }

        public SimpleUser Winner { get; set; }
        public SimpleUser Loser { get; set; }
        public List<string> BattleLog { get; set; }
    }
}