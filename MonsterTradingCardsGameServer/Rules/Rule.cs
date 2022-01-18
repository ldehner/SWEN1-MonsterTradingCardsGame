using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.Rules
{
    /// <summary>
    ///     Rule
    /// </summary>
    public abstract class Rule
    {
        /// <summary>
        ///     Calculates the damage between two cards
        /// </summary>
        /// <param name="card1">first card</param>
        /// <param name="card2">second card</param>
        public abstract void CalculateDamage(Card card1, Card card2);
    }
}