using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.Rules
{
    public class DragonFireElveRule : Rule
    {
        public override void CalculateDamage(Card card1, Card card2)
        {
            if (card2.GetType() != typeof(Monster)) return;
            if (((Monster) card2).Type == MonsterType.Elve && card2.Mod == Modification.Fire) card1.Damage *= 0;
        }
    }
}