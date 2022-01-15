using System;

namespace MonsterTradingCardsGameServer.Battles
{
    /// <summary>
    /// Exception if a deck is invalid
    /// </summary>
    public class InvalidDeckException : Exception
    {
        public InvalidDeckException()
        {
        }

        public InvalidDeckException(string message) : base(message)
        {
        }

        public InvalidDeckException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}