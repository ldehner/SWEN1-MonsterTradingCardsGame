using System;

namespace MonsterTradingCardsGameServer.Cards
{
    public class TooFewCoinsException: Exception
    {
        public TooFewCoinsException()
        {
        }

        public TooFewCoinsException(string message)
            : base(message)
        {
        }

        public TooFewCoinsException(string message, Exception inner)
            : base(message, inner)
        {
        }
        
    }
}