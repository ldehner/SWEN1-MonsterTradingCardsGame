using MonsterTradingCardsGameServer.Rules;

namespace MonsterTradingCardsGameServer.Cards
{
    public class CardRuleAdder
    {
        private Card _card;

        public CardRuleAdder(Card card)
        {
            _card = card;
        }

        public void AddRules()
        {
            if (_card.GetType() == typeof(Monster))
            {
                switch (((Monster) _card).Type)
                {
                    case MonsterType.Dragon:
                        _card.Rules.Add(new DragonFireElveRule());
                        break;
                    case MonsterType.Elve:
                        break;
                    case MonsterType.Goblin:
                        _card.Rules.Add(new GoblinDragonRule());
                        break;
                    case MonsterType.Knight:
                        _card.Rules.Add(new KnightWaterRule());
                        break;
                    case MonsterType.Kraken:
                        break;
                    case MonsterType.Org:
                        _card.Rules.Add(new OrgWizardRule());
                        break;
                    case MonsterType.Wizard:
                        break;
                    default:
                        throw new CardNotFoundException();
                        break;
                }
            }

            switch (_card.Mod)
            {
                case Modification.Water:
                    _card.Rules.Add(new WaterRule());
                    break;
                case Modification.Fire:
                    _card.Rules.Add(new FireRule());
                    break;
                case Modification.Normal:
                    _card.Rules.Add(new NormalRule());
                    break;
                default:
                    break;
            }
        }
    }
}