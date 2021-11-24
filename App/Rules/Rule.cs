using System.Runtime.CompilerServices;
using MonsterTradingCardsGame.App.Cards;

namespace MonsterTradingCardsGame.App.Rules
{
    public abstract class Rule
    {
        public abstract void CalculateDamage(Card card1, Card card2);
    }
}