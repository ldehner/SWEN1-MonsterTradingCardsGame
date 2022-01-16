namespace MonsterTradingCardsGameServer.Trades
{
    /// <summary>
    /// 
    /// </summary>
    public class ReadableTrade
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tradeId"></param>
        /// <param name="username"></param>
        /// <param name="cardName"></param>
        /// <param name="damage"></param>
        /// <param name="requiredType"></param>
        /// <param name="requiredDamage"></param>
        public ReadableTrade(string tradeId, string username, string cardName, double damage, string requiredType,
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