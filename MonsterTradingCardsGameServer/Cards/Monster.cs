using System;
using MonsterTradingCardsGameServer.Rules;

namespace MonsterTradingCardsGameServer.Cards
{
    /// <summary>
    /// Monster Card
    /// </summary>
    public class Monster : Card
    {
        /// <summary>
        /// Sets all needed attributes
        /// </summary>
        /// <param name="id">uid of the card</param>
        /// <param name="damage">damage of the card</param>
        /// <param name="mod">modification of the card</param>
        /// <param name="type">the monster type of the card</param>
        public Monster(Guid id, double damage, Modification mod, MonsterType type) : base(id, damage, mod)
        {
            Type = type;
            SetRules();
        }

        public MonsterType Type { get; }

        /// <summary>
        /// Converts the monster card into a universal card
        /// </summary>
        /// <returns>a universal card</returns>
        public override UniversalCard ToUniversalCard()
        {
            return new UniversalCard(Id.ToString(), Mod, Type, Damage);
        }

        /// <summary>
        /// Builds the card name
        /// </summary>
        /// <returns>the card name</returns>
        public override string GetCardName()
        {
            return Mod + "-" + Type;
        }

        /// <summary>
        /// Returns the card type
        /// </summary>
        /// <returns>card type</returns>
        public override string GetCardType()
        {
            return "Monster";
        }

        /// <summary>
        /// Applies the card specific rule
        /// </summary>
        /// <exception cref="CardNotFoundException">Exception in case the card wasn't found</exception>
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
                    Rules.Add(new KrakenSpellRule());
                    break;
                case MonsterType.Org:
                    Rules.Add(new OrgWizardRule());
                    break;
                case MonsterType.Wizard:
                    break;
                default:
                    throw new CardNotFoundException();
            }
        }
    }
}