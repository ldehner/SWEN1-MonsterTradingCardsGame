using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.Rules
{
    /// <summary>
    /// Normal rule
    /// </summary>
    public class NormalRule : Rule
    {
        /// <summary>
        /// Calculates the damage between two cards
        /// </summary>
        /// <param name="card1">first card</param>
        /// <param name="card2">second card</param>
        public override void CalculateDamage(Card card1, Card card2)
        {
            switch (card2.Mod)
            {
                case Modification.Water:
                    card1.Damage *= 2;
                    break;
                case Modification.Fire:
                    card1.Damage /= 2;
                    break;
            }
        }
    }
}