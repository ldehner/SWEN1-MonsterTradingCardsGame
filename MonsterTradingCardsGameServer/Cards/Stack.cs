using System.Collections.Generic;

namespace MonsterTradingCardsGameServer.Cards
{
    public class Stack : CardCollection
    {
        public Stack(List<Card> cards) : base(cards)
        {}

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }
    }
}