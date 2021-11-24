using System;

namespace MonsterTradingCardsGame.App.Rules
{
    public class KnightRule : Rule
    {
        public override void CalculateDamage(Card card1, Card card2)
        {
            if (card2.GetType() == typeof(Spell) && card2.Mod == Modification.Water)
            {
                card1.Damage *= 0;
            }
        }
    }
}