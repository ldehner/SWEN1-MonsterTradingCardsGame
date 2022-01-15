namespace MonsterTradingCardsGameServer.Cards
{
    public class ReadableCard
    {
        public ReadableCard(string id, string cardName, double damage)
        {
            Id = id;
            CardName = cardName;
            Damage = damage;
        }

        public string Id { get; set; }
        public string CardName { get; set; }
        public double Damage { get; set; }
    }
}