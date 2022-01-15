namespace MonsterTradingCardsGameServer
{
    public class TradingOffer
    {
        public TradingOffer(string tradeId, string username, string cardName, double damage, string requiredType,
            double requiredDamage)
        {
            TradeId = tradeId;
            Trader = username;
            CardName = cardName;
            Damage = damage;
            RequiredType = requiredType;
            RequiredDamage = requiredDamage;
        }

        public string TradeId { get; set; }
        public string Trader { get; set; }
        public string CardName { get; set; }
        public double Damage { get; set; }
        public string RequiredType { get; set; }
        public double RequiredDamage { get; set; }
    }
}