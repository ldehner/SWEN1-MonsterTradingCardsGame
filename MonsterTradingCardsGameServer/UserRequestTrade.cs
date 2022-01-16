namespace MonsterTradingCardsGameServer
{
    public class UserRequestTrade
    {
        public string CardToTrade;
        public string Id;
        public double MinimumDamage;
        public string Type;

        public UserRequestTrade(string id, string cardToTrade, string type, double minimumDamage)
        {
            Id = id;
            CardToTrade = cardToTrade;
            Type = type;
            MinimumDamage = minimumDamage;
        }
    }
}