using System.Collections.Generic;

namespace MonsterTradingCardsGameServer.Cards
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

        public List<UniversalCard> ToUniversalCardList()
        {
            var list = new List<UniversalCard>();
            Cards.ForEach(card => list.Add(card.ToUniversalCard()));
            return list;
        }
    }
}