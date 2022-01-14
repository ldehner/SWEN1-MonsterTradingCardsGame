using System;
using MonsterTradingCardsGameServer.Rules;

namespace MonsterTradingCardsGameServer.Cards
{
    public class Monster:Card
    {
        public MonsterType Type { get; }
        
        public Monster(Guid id, int damage, Modification mod, MonsterType type) : base(id, damage, mod)
        {
            Type = type;
            SetRules();
        }

        public override UniversalCard ToUniversalCard()
        {
            return new UniversalCard(Id.ToString(), Mod, Type, Damage);
        }

        public override string GetCardName()
        {
            return Mod + "-" + Type;
        }

        public override void SetRules()
        {
            base.SetRules();
            switch (Type)
            {
                case MonsterType.Dragon:
                    Rules.Add(new DragonFireElveRule());
                    break;
                case MonsterType.Elve:
                    break;
                case MonsterType.Goblin:
                    Rules.Add(new GoblinDragonRule());
                    break;
                case MonsterType.Knight:
                    Rules.Add(new KnightWaterRule());
                    break;
                case MonsterType.Kraken:
                    break;
                case MonsterType.Org:
                    Rules.Add(new OrgWizardRule());
                    break;
                case MonsterType.Wizard:
                    break;
                default:
                    throw new CardNotFoundException();
                    break;
            }
        }
    }
}