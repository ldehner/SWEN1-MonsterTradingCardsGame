using System;

namespace MonsterTradingCardsGameServer.Battles
{
    /// <summary>
    /// Exception in case a battle wasn't found after a search
    /// </summary>
    public class BattleNotFoundException : Exception
    {
        public BattleNotFoundException()
        {
        }

        public BattleNotFoundException(string message) : base(message)
        {
        }

        public BattleNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}