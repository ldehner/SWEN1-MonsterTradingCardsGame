namespace MonsterTradingCardsGameServer
{
    public class TradingOffer
    {
        public string TradeId { get; set; }
        public string CardName { get; set; }
        public double Damage { get; set; }
        public string RequiredType { get; set; }
        public double RequiredDamage { get; set; }

        public TradingOffer(string tradeId, string cardName, double damage, string requiredType, double requiredDamage)
        {
            TradeId = tradeId;
            CardName = cardName;
            Damage = damage;
            RequiredType = requiredType;
            RequiredDamage = requiredDamage;
        }
    }
}