using System;

namespace MonsterTradingCardsGameServer.Battles
{
    public class InvalidDeckException : Exception
    {
        public InvalidDeckException(){
        }

        public InvalidDeckException(string message) : base(message)
        {
        }

        public InvalidDeckException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}