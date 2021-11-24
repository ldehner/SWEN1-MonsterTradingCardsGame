using System.Collections.Generic;

namespace MonsterTradingCardsGame.App.Cards
{
    public class Package
    {
        public List<Card> Cards { get; private set; }

        public Package()
        {
            Cards = new List<Card>();
        }
    }
}