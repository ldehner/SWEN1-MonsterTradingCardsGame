using System;
using MonsterTradingCardsGameServer.Cards;
using NUnit.Framework;

namespace MonsterTradingCardsGameServer.Test
{
    /// <summary>
    ///     Checks if the card names are generated successfully
    /// </summary>
    [TestFixture]
    public class CardNameTest
    {
        /// <summary>
        ///     Checks if monster card name is generated successfully
        /// </summary>
        /// <param name="damage">damage of the card</param>
        /// <param name="mod">modification of the card</param>
        /// <param name="type">type of the card</param>
        /// <param name="expName">Expected name</param>
        [TestCase(100.5, Modification.Fire, MonsterType.Dragon, "Fire-Dragon")]
        [TestCase(34.4, Modification.Water, MonsterType.Wizard, "Water-Wizard")]
        [TestCase(65.6, Modification.Normal, MonsterType.Goblin, "Normal-Goblin")]
        [TestCase(32.0, Modification.Fire, MonsterType.Elf, "Fire-Elf")]
        [TestCase(43.1, Modification.Water, MonsterType.Knight, "Water-Knight")]
        [TestCase(87.2, Modification.Normal, MonsterType.Kraken, "Normal-Kraken")]
        [TestCase(73.9, Modification.Fire, MonsterType.Ork, "Fire-Ork")]
        public void CheckIfMonsterCardNameIsGeneratedSuccessful(double damage, Modification mod, MonsterType type,
            string expName)
        {
            // arrange
            var card = new Monster(Guid.NewGuid(), damage, mod, type);

            // act
            var name = card.GetCardName();

            // assert
            Assert.AreEqual(expName, name);
        }

        /// <summary>
        ///     Checks if spell card name is generated successfully
        /// </summary>
        /// <param name="damage">damage of the card</param>
        /// <param name="mod">modification of the card</param>
        /// <param name="expName">expected card name</param>
        [TestCase(100.5, Modification.Fire, "Fire-Spell")]
        [TestCase(12.2, Modification.Water, "Water-Spell")]
        [TestCase(54.9, Modification.Normal, "Normal-Spell")]
        public void CheckIfSpellCardNameIsGeneratedSuccessful(double damage, Modification mod, string expName)
        {
            // arrange
            var card = new Spell(Guid.NewGuid(), damage, mod);

            // act
            var name = card.GetCardName();

            // assert
            Assert.AreEqual(expName, name);
        }
    }
}