using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.Rules
{
    /// <summary>
    /// Sets damage of dragon to zero if other card is fire elve
    /// </summary>
    public class DragonFireElveRule : Rule
    {
        /// <summary>
        /// Calculates the damage between two cards
        /// </summary>
        /// <param name="card1">first card</param>
        /// <param name="card2">second card</param>
        public override void CalculateDamage(Card card1, Card card2)
        {
            if (card2.GetType() != typeof(Monster)) return;
            if (((Monster) card2).Type == MonsterType.Elve && card2.Mod == Modification.Fire) card1.Damage *= 0;
        }
    }
}