using System;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Rules;
using NUnit.Framework;

namespace MonsterTradingCardsGameServer.Test
{
    /// <summary>
    /// Checks if normal rule works
    /// </summary>
    [TestFixture]
    public class NormalRuleTest
    {
        /// <summary>
        /// Checks if normal damage is halved if other card is fire
        /// </summary>
        /// <param name="Dmg1">damage of card 1</param>
        /// <param name="Dmg2">damage of card 2</param>
        [TestCase(10, 1)]
        [TestCase(20, 100)]
        [TestCase(100, 51)]
        public void CheckIfNormalDamageIsHalvedWhenOtherCardHasFireMod(int Dmg1, int Dmg2)
        {
            // arrange
            var card1 = new Spell(Guid.NewGuid(), Dmg1, Modification.Normal);
            var card2 = new Spell(Guid.NewGuid(), Dmg2, Modification.Fire);

            // act
            new NormalRule().CalculateDamage(card1, card2);

            // assert
            Assert.AreEqual(Dmg1 / 2, card1.Damage);
        }

        /// <summary>
        /// Checks if normal damage is doubled if other card is water
        /// </summary>
        /// <param name="Dmg1">damage of card 1</param>
        /// <param name="Dmg2">damage of card 2</param>
        [TestCase(10, 1)]
        [TestCase(20, 100)]
        [TestCase(100, 51)]
        public void CheckIfNormalDamageIsDoubledWhenOtherCardHasWaterMod(int Dmg1, int Dmg2)
        {
            // arrange
            var card1 = new Spell(Guid.NewGuid(), Dmg1, Modification.Normal);
            var card2 = new Spell(Guid.NewGuid(), Dmg2, Modification.Water);

            // act
            new NormalRule().CalculateDamage(card1, card2);

            // assert
            Assert.AreEqual(Dmg1 * 2, card1.Damage);
        }

        /// <summary>
        /// Checks if normal damage is same if other card is normal
        /// </summary>
        /// <param name="Dmg1">damage of card 1</param>
        /// <param name="Dmg2">damage of card 2</param>
        [TestCase(10, 1)]
        [TestCase(20, 100)]
        [TestCase(100, 51)]
        public void CheckIfNormalDamageIsSameWhenOtherCardHasNormalMod(int Dmg1, int Dmg2)
        {
            // arrange
            var card1 = new Spell(Guid.NewGuid(), Dmg1, Modification.Normal);
            var card2 = new Spell(Guid.NewGuid(), Dmg2, Modification.Normal);

            // act
            new NormalRule().CalculateDamage(card1, card2);

            // assert
            Assert.AreEqual(Dmg1, card1.Damage);
        }
    }
}