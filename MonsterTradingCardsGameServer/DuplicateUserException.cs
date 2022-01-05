using System;

namespace MonsterTradingCardsGameServer
{
    public class DuplicateUserException : Exception
    {
        public DuplicateUserException()
        {
        }

        public DuplicateUserException(string message) : base(message)
        {
        }

        public DuplicateUserException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}