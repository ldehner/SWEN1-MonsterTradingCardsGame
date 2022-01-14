using System;

namespace MonsterTradingCardsGameServer.Cards
{
    public class Monster:Card
    {
        public MonsterType Type { get; }
        
        public Monster(Guid id, int damage, Modification mod, MonsterType type) : base(id, damage, mod)
        {
            Type = type;
        }

        public override UniversalCard ToUniversalCard()
        {
            return new UniversalCard(Id.ToString(), Mod, Type, Damage);
        }

        public override string GetCardName()
        {
            return Mod + "-" + Type;
        }
    }
}