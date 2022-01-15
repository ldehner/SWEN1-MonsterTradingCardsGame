using System;
using System.Collections.Generic;

namespace MonsterTradingCardsGameServer.Cards
{
    public class Package
    {
        public readonly Guid Guid;

        public Package(List<Card> cards)
        {
            Cards = cards;
            Guid = Guid.NewGuid();
        }

        public List<Card> Cards { get; }
    }
}