using System;

namespace MonsterTradingCardsGameServer.Rules
{
    /// <summary>
    ///     Exception in case rule does not exist
    /// </summary>
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