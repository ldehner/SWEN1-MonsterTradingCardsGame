using System;

namespace MonsterTradingCardsGameServer.Cards
{
    /// <summary>
    /// Exception in case a card wasn't cound
    /// </summary>
    public class CardNotFoundException : Exception
    {
        public CardNotFoundException()
        {
        }

        public CardNotFoundException(string message)
            : base(message)
        {
        }

        public CardNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}