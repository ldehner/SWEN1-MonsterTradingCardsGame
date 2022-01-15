using System.Collections.Generic;

namespace MonsterTradingCardsGameServer.Cards
{
    public class Deck : CardCollection
    {
        public Deck(List<Card> cards) : base(cards)
        {
        }
    }
}