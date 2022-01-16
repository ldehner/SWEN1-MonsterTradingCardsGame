using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.Trades
{
    /// <summary>
    /// Class for trades
    /// </summary>
    public class UniversalTrade
    {
        /// <summary>
        /// Sets all parameters
        /// </summary>
        /// <param name="username">traders username</param>
        /// <param name="card">card to trade</param>
        /// <param name="requiredType">required card type</param>
        /// <param name="requiredDamage">required damage</param>
        public UniversalTrade(string username, Card card, string requiredType, double requiredDamage)
        {
            Trader = username;
            Card = card;
            RequiredType = requiredType;
            RequiredDamage = requiredDamage;
        }

        public string Trader { get; set; }
        public Card Card { get; set; }
        public string RequiredType { get; set; }
        public double RequiredDamage { get; set; }
    }
}