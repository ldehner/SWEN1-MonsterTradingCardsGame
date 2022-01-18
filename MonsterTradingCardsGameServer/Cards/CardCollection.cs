using System.Collections.Generic;

namespace MonsterTradingCardsGameServer.Cards
{
    /// <summary>
    ///     base for stack and deck
    /// </summary>
    public class CardCollection
    {
        /// <summary>
        ///     sets all needed attributes
        /// </summary>
        /// <param name="cards">the list of cards</param>
        public CardCollection(List<Card> cards)
        {
            Cards = cards;
        }

        public List<Card> Cards { get; set; }

        /// <summary>
        ///     converts the card list into a universal card list
        /// </summary>
        /// <returns>a universal card list</returns>
        public List<UniversalCard> ToUniversalCardList()
        {
            var list = new List<UniversalCard>();
            Cards.ForEach(card => list.Add(card.ToUniversalCard()));
            return list;
        }

        /// <summary>
        ///     converts the card list into a readable card list
        /// </summary>
        /// <returns>a readable card list</returns>
        public List<ReadableCard> ToReadableCardList()
        {
            var cards = new List<ReadableCard>();
            Cards.ForEach(card => cards.Add(card.ToReadableCard()));
            return cards;
        }
    }
}