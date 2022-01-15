using System.Collections.Generic;

namespace MonsterTradingCardsGameServer.Cards
{
    /// <summary>
    /// Stack
    /// </summary>
    public class Stack : CardCollection
    {
        /// <summary>
        /// Sets the attributes
        /// </summary>
        /// <param name="cards">list of cards</param>
        public Stack(List<Card> cards) : base(cards)
        {
        }
    }
}