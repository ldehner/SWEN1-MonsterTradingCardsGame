using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.Rules
{
    public abstract class Rule
    {
        public abstract void CalculateDamage(Card card1, Card card2);
    }
}