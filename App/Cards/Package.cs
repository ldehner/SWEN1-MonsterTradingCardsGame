using System.Collections.Generic;

namespace MonsterTradingCardsGame.App
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