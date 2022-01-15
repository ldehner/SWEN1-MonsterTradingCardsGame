using System;

namespace MonsterTradingCardsGameServer.Cards
{
    public class UniversalCard
    {
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

        public Card ToCard()
        {
            Card card = MonsterType == MonsterType.None
                ? new Spell(Id, Damage, Modification)
                : new Monster(Id, Damage, Modification, MonsterType);
            return card;
        }
    }
}