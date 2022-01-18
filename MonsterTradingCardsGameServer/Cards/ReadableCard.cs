namespace MonsterTradingCardsGameServer.Cards
{
    /// <summary>
    ///     Readable version of a card
    /// </summary>
    public class ReadableCard
    {
        /// <summary>
        ///     sets all nedded attributes
        /// </summary>
        /// <param name="id">uid of the card</param>
        /// <param name="cardName">name of the card</param>
        /// <param name="damage">damage of the card</param>
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