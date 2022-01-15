using System;

namespace MonsterTradingCardsGameServer.Cards
{
    /// <summary>
    /// Universal card
    /// </summary>
    public class UniversalCard
    {
        /// <summary>
        /// Sets all attributes
        /// </summary>
        /// <param name="id">uid of the card</param>
        /// <param name="modification">modification of the card</param>
        /// <param name="monsterType">monstertype of the card</param>
        /// <param name="damage">damage of the card</param>
        public UniversalCard(string id, Modification modification, MonsterType monsterType, double damage)
        {
            Id = Guid.Parse(id);
            Modification = modification;
            MonsterType = monsterType;
            Damage = damage;
        }

        public Guid Id { get; set; }
        public Modification Modification { get; set; }
        public MonsterType MonsterType { get; set; }
        public double Damage { get; set; }

        /// <summary>
        /// Converts the universal card into a card
        /// </summary>
        /// <returns>the card</returns>
        public Card ToCard()
        {
            Card card = MonsterType == MonsterType.None
                ? new Spell(Id, Damage, Modification)
                : new Monster(Id, Damage, Modification, MonsterType);
            return card;
        }
    }
}