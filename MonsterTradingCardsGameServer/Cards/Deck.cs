using System.Collections.Generic;

namespace MonsterTradingCardsGameServer.Cards
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        public Deck(List<Card> cards)
        {
            Cards = cards;
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }
    }
}