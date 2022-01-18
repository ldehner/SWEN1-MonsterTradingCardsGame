using System;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Rules;
using NUnit.Framework;

namespace MonsterTradingCardsGameServer.Test
{
    /// <summary>
    ///     Checks if complex rules work
    /// </summary>
    [TestFixture]
    public class OtherRulesTest
    {
        /// <summary>
        ///     Checks if knight's damage is zero, when other card is a water spell
        /// </summary>
        [Test]
        public void CheckIfKnightsDamageIsZeroWhenOtherCardIsWaterSpell()
        {
            // arrange
            var knight = new Monster(Guid.NewGuid(), 10, Modification.Normal, MonsterType.Knight);
            var waterSpell = new Spell(Guid.NewGuid(), 10, Modification.Water);

            // act
            new KnightWaterRule().CalculateDamage(knight, waterSpell);

            // assert
            Assert.AreEqual(0.0, knight.Damage);
            Assert.AreEqual(10.0, waterSpell.Damage);
        }

        /// <summary>
        ///     Checks if spell's damage is zero, when other card is a kraken
        /// </summary>
        [Test]
        public void CheckIfSpellsDamageIsZeroWhenOtherCardIsKraken()
        {
            // arrange
            var kraken = new Monster(Guid.NewGuid(), 10, Modification.Normal, MonsterType.Kraken);
            var waterSpell = new Spell(Guid.NewGuid(), 10, Modification.Water);

            // act
            new KrakenSpellRule().CalculateDamage(kraken, waterSpell);

            // assert
            Assert.AreEqual(10.0, kraken.Damage);
            Assert.AreEqual(0.0, waterSpell.Damage);
        }

        /// <summary>
        ///     Checks if ork's damage is zero, when other card is a wizard
        /// </summary>
        [Test]
        public void CheckIfOrksDamageIsZeroWhenOtherCardIsWizard()
        {
            // arrange
            var ork = new Monster(Guid.NewGuid(), 10, Modification.Normal, MonsterType.Ork);
            var wizard = new Monster(Guid.NewGuid(), 10, Modification.Normal, MonsterType.Wizard);

            // act
            new OrkWizardRule().CalculateDamage(ork, wizard);

            // assert
            Assert.AreEqual(0.0, ork.Damage);
            Assert.AreEqual(10.0, wizard.Damage);
        }

        /// <summary>
        ///     Checks if goblin's damage is zero, when other card is a dragon
        /// </summary>
        [Test]
        public void CheckIfGoblinsDamageIsZeroWhenOtherCardIsDragon()
        {
            // arrange
            var goblin = new Monster(Guid.NewGuid(), 10, Modification.Normal, MonsterType.Goblin);
            var dragon = new Monster(Guid.NewGuid(), 10, Modification.Normal, MonsterType.Dragon);

            // act
            new GoblinDragonRule().CalculateDamage(goblin, dragon);

            // assert
            Assert.AreEqual(0.0, goblin.Damage);
            Assert.AreEqual(10.0, dragon.Damage);
        }

        /// <summary>
        ///     Checks if dragon's damage is zero, when other card is a fire elf
        /// </summary>
        [Test]
        public void CheckIfDragonsDamageIsZeroWhenOtherCardIsFireElf()
        {
            // arrange
            var dragon = new Monster(Guid.NewGuid(), 10, Modification.Normal, MonsterType.Dragon);
            var fireElf = new Monster(Guid.NewGuid(), 10, Modification.Fire, MonsterType.Elf);

            // act
            new DragonFireElfRule().CalculateDamage(dragon, fireElf);

            // assert
            Assert.AreEqual(0.0, dragon.Damage);
            Assert.AreEqual(10.0, fireElf.Damage);
        }
    }
}