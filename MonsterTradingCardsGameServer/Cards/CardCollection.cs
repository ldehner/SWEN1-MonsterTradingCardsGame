using System.Collections.Generic;

namespace MonsterTradingCardsGameServer.Cards
{
    public class CardCollection
    {
        public List<Card> Cards { get; set; }

        public CardCollection(List<Card> cards)
        {
            Cards = cards;
        }
        
        public List<UniversalCard> ToUniversalCardList()
        {
            var list = new List<UniversalCard>();
            Cards.ForEach(card => list.Add(card.ToUniversalCard()));
            return list;
        }

        public List<ReadableCard> ToReadableCardList()
        {
            var cards = new List<ReadableCard>();
            Cards.ForEach(card => cards.Add(card.ToReadableCard()));
            return cards;
        }
    }
}