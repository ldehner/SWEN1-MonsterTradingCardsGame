using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.Rules
{
    public class OrgWizardRule : Rule
    {
        public override void CalculateDamage(Card card1, Card card2)
        {
            if (card2.GetType() != typeof(Monster)) return;
            if (((Monster) card2).Type == MonsterType.Wizard) card1.Damage *= 0;
        }
    }
}