using MonsterTradingCardsGame.App.Cards;

namespace MonsterTradingCardsGame.App.Rules
{
    public class FireRule : Rule
    {

        public override void CalculateDamage(Card card1, Card card2)
        {
            switch (card2.Mod)
            {
                case Modification.Normal:
                    card1.Damage *= 2;
                    break;
                case Modification.Water:
                    card1.Damage /= 2;
                    break;
            }
        }
    }
}