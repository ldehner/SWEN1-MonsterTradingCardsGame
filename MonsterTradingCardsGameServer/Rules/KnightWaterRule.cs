using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.Rules
{
    /// <summary>
    ///     sets damage of knight to zero if other card is water spell
    /// </summary>
    public class KnightWaterRule : Rule
    {
        /// <summary>
        ///     Calculates the damage between two cards
        /// </summary>
        /// <param name="card1">first card</param>
        /// <param name="card2">second card</param>
        public override void CalculateDamage(Card card1, Card card2)
        {
            if (card2.GetType() == typeof(Spell) && card2.Mod == Modification.Water) card1.Damage *= 0;
        }
    }
}