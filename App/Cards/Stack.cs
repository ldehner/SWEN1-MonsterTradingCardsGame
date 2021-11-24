using System.Collections.Generic;

namespace MonsterTradingCardsGame.App.Cards
{
    public class Stack
    {
        public List<Card> Cards { get; set; }

        public Stack(List<Card> cards)
        {
            Cards = cards;
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }
    }
}