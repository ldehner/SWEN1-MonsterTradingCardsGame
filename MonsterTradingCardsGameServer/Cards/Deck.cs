using System.Collections.Generic;

namespace MonsterTradingCardsGameServer.Cards
{
    /// <summary>
    ///     the deck of an user
    /// </summary>
    public class Deck : CardCollection
    {
        public Deck(List<Card> cards) : base(cards)
        {
        }
    }
}