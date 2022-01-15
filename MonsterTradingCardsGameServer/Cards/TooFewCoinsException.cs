using System;

namespace MonsterTradingCardsGameServer.Cards
{
    /// <summary>
    /// Exception in case user has not enough coins
    /// </summary>
    public class TooFewCoinsException : Exception
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