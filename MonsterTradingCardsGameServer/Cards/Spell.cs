using System;

namespace MonsterTradingCardsGameServer.Cards
{
    public class Spell:Card
    {
        public Spell(Guid id, int damage, Modification mod):base(id, damage, mod)
        {
            
        }
        
        public override UniversalCard ToUniversalCard()
        {
            return new UniversalCard(Id.ToString(), Mod, MonsterType.None, Damage);
        }

        public override string GetCardName()
        {
            return Mod + "-Spell";
        }
    }
}