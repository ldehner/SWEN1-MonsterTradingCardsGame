namespace MonsterTradingCardsGameServer
{
    public class TradingDeal
    {
        public string Id;
        public string CardToTrade;
        public string Type;
        public double MinimumDamage;

        public TradingDeal(string id, string cardToTrade, string type, double minimumDamage)
        {
            Id = id;
            CardToTrade = cardToTrade;
            Type = type;
            MinimumDamage = minimumDamage;
        }
    }
}