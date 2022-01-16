using System;

namespace MonsterTradingCardsGameServer.Battles
{
    public class AlreadyInQueueException : Exception
    {
        public AlreadyInQueueException()
        {
        }

        public AlreadyInQueueException(string message) : base(message)
        {
        }

        public AlreadyInQueueException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}