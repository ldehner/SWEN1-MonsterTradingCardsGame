using System;
using System.Collections.Generic;

namespace MonsterTradingCardsGameServer.Cards
{
    /// <summary>
    /// Package
    /// </summary>
    public class Package
    {
        public readonly Guid Guid;

        /// <summary>
        /// sets the list of cards
        /// </summary>
        /// <param name="cards">List with four cards</param>
        public Package(List<Card> cards)
        {
            Cards = cards;
            Guid = Guid.NewGuid();
        }

        public List<Card> Cards { get; }
    }
}