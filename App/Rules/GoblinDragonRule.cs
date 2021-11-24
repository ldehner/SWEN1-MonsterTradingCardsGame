using MonsterTradingCardsGame.App.Cards;

namespace MonsterTradingCardsGame.App.Rules
{
    public class GoblinDragonRule:Rule
    {
        public override void CalculateDamage(Card card1, Card card2)
        {
            if (card2.GetType() != typeof(Monster)) return;
            if (((Monster) card2).Type == MonsterType.Dragon)
            {
                card1.Damage *= 0;
            }
        }
    }
}