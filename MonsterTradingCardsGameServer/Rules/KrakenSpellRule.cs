using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.Rules
{
    /// <summary>
    /// sets spell damage to zero
    /// </summary>
    public class KrakenSpellRule : Rule
    {
        /// <summary>
        /// Calculates the damage between two cards
        /// </summary>
        /// <param name="card1">first card</param>
        /// <param name="card2">second card</param>
        public override void CalculateDamage(Card card1, Card card2)
        {
            if (card2.GetType() != typeof(Spell)) return;
            card2.Damage *= 0;
        }
    }
}