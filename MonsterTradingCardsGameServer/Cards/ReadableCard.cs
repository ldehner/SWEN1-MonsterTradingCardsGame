namespace MonsterTradingCardsGameServer.Cards
{
    public class ReadableCard
    {
        public string Id { get; set; }
        public string CardName { get; set; }
        public double Damage { get; set; }

        public ReadableCard(string id, string cardName, double damage)
        {
            Id = id;
            CardName = cardName;
            Damage = damage;
        }
    }
}