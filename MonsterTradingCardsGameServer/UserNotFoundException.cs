using System;

namespace MonsterTradingCardsGameServer
{
    /// <summary>
    /// Exception in case user wasn't found
    /// </summary>
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}