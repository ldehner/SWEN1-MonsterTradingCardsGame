using System;

namespace MonsterTradingCardsGameServer.Cards
{
    /// <summary>
    /// Exception in case there are no more packages
    /// </summary>
    public class NoMorePackagesException : Exception
    {
        public NoMorePackagesException()
        {
        }

        public NoMorePackagesException(string message)
            : base(message)
        {
        }

        public NoMorePackagesException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}