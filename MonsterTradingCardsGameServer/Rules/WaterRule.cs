using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.Rules
{
    public class WaterRule : Rule
    {
        public override void CalculateDamage(Card card1, Card card2)
        {
            switch (card2.Mod)
            {
                case Modification.Fire:
                    card1.Damage *= 2;
                    break;
                case Modification.Normal:
                    card1.Damage /= 2;
                    break;
            }
        }
    }
}