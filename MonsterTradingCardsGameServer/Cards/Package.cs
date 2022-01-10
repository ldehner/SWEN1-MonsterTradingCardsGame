using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Cards
{
    public class Package
    {
        public List<Card> Cards { get; private set; }
        public readonly Guid Guid;

        public Package(List<Card> cards)
        {
            Cards = cards;
            Guid = Guid.NewGuid();
        }
    }
}