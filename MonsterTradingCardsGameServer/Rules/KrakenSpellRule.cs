using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.Rules
{
    public class KrakenSpellRule : Rule
    {
        public override void CalculateDamage(Card card1, Card card2)
        {
            if (card2.GetType() != typeof(Spell)) return;
            card2.Damage *= 0;
        }
    }
}