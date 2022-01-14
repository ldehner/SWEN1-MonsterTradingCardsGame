using System.Collections.Generic;
using MonsterTradingCardsGameServer.Rules;

namespace MonsterTradingCardsGameServer.Cards
{
    public static class CardRuleAdder
    {
        public static List<Rule> AddRules(Card card)
        {
            var rules = new List<Rule>();
            if (card.GetType() == typeof(Monster))
            {
                switch (((Monster) card).Type)
                {
                    case MonsterType.Dragon:
                        rules.Add(new DragonFireElveRule());
                        break;
                    case MonsterType.Elve:
                        break;
                    case MonsterType.Goblin:
                        rules.Add(new GoblinDragonRule());
                        break;
                    case MonsterType.Knight:
                        rules.Add(new KnightWaterRule());
                        break;
                    case MonsterType.Kraken:
                        break;
                    case MonsterType.Org:
                        rules.Add(new OrgWizardRule());
                        break;
                    case MonsterType.Wizard:
                        break;
                    case MonsterType.None:
                    default:
                        throw new CardNotFoundException();
                        break;
                }
            }

            switch (card.Mod)
            {
                case Modification.Water:
                    rules.Add(new WaterRule());
                    break;
                case Modification.Fire:
                    rules.Add(new FireRule());
                    break;
                case Modification.Normal:
                    rules.Add(new NormalRule());
                    break;
                case Modification.None:
                default:
                    break;
            }

            return rules;
        }
    }
}