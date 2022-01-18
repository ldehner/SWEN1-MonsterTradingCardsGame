using System;

namespace MonsterTradingCardsGameServer.Users
{
    /// <summary>
    ///     Exception in case user already exists
    /// </summary>
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