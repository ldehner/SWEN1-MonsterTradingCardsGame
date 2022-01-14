using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer
{
    public class Trade
    {
        public string Trader { get; set; }
        public Card Card { get; set; }
        public string RequiredType { get; set; }
        public double RequiredDamage { get; set; }

        public Trade(string username, Card card, string requiredType, double requiredDamage)
        {
            Trader = username;
            Card = card;
            RequiredType = requiredType;
            RequiredDamage = requiredDamage;
        }
    }
}