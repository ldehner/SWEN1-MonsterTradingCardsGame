using System;

namespace MonsterTradingCardsGameServer.Rules
{
    public class NoSuchRuleException : Exception
    {
        public NoSuchRuleException()
        {
        }

        public NoSuchRuleException(string message)
            : base(message)
        {
        }

        public NoSuchRuleException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}