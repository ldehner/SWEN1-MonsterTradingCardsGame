using System;

namespace MonsterTradingCardsGameServer.Battles
{
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