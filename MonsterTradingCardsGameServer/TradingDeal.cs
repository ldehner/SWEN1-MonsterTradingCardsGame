namespace MonsterTradingCardsGameServer
{
    public class TradingDeal
    {
        public string CardToTrade;
        public string Id;
        public double MinimumDamage;
        public string Type;

        public TradingDeal(string id, string cardToTrade, string type, double minimumDamage)
        {
            Id = id;
            CardToTrade = cardToTrade;
            Type = type;
            MinimumDamage = minimumDamage;
        }
    }
}