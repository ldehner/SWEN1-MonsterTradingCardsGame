using System;

namespace MonsterTradingCardsGameServer.Cards
{
    /// <summary>
    ///     Spell card
    /// </summary>
    public class Spell : Card
    {
        /// <summary>
        ///     sets all needed attributes
        /// </summary>
        /// <param name="id">uid of the card</param>
        /// <param name="damage">damage of the card</param>
        /// <param name="mod">modification of the card</param>
        public Spell(Guid id, double damage, Modification mod) : base(id, damage, mod)
        {
            base.SetRules();
        }

        /// <summary>
        ///     converts the spell card into a universal card
        /// </summary>
        /// <returns>a universal card</returns>
        public override UniversalCard ToUniversalCard()
        {
            return new UniversalCard(Id.ToString(), Mod, MonsterType.None, Damage);
        }

        /// <summary>
        ///     Generates the card name
        /// </summary>
        /// <returns>the card name</returns>
        public override string GetCardName()
        {
            return Mod + "-Spell";
        }

        /// <summary>
        ///     Returns the card type
        /// </summary>
        /// <returns>card type</returns>
        public override string GetCardType()
        {
            return "Spell";
        }
    }
}