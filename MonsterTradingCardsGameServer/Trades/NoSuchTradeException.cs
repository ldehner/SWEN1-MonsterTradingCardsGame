using System;

namespace MonsterTradingCardsGameServer.Trades
{
    public class NoSuchTradeException:Exception
    {
        public NoSuchTradeException()
        {
        }

        public NoSuchTradeException(string message) : base(message)
        {
        }

        public NoSuchTradeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}