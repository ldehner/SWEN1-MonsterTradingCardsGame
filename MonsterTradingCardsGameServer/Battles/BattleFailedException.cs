using System;

namespace MonsterTradingCardsGameServer.Battles
{
    public class BattleFailedException : Exception
    {
        public BattleFailedException()
        {
        }

        public BattleFailedException(string message) : base(message)
        {
        }

        public BattleFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}