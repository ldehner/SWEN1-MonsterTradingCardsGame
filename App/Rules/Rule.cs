using System.Runtime.CompilerServices;

namespace MonsterTradingCardsGame.App
{
    public abstract class Rule
    {
        public abstract void CalculateDamage(Card card1, Card card2);
    }
}