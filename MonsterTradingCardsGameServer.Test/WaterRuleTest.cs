using System;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Rules;
using NUnit.Framework;

namespace MonsterTradingCardsGameServer.Test
{
    /// <summary>
    /// Checks if water rule works
    /// </summary>
    [TestFixture]
    public class WaterRuleTest
    {
        
        /// <summary>
        /// Checks if water damage is halved if other card is normal
        /// </summary>
        /// <param name="Dmg1">damage of card 1</param>
        /// <param name="Dmg2">damage of card 2</param>
        [TestCase(10, 1)]
        [TestCase(20, 100)]
        [TestCase(100, 51)]
        public void CheckIfWaterDamageIsHalvedWhenOtherCardHasNormalMod(int Dmg1, int Dmg2)
        {
            // arrange
            var card1 = new Spell(Guid.NewGuid(), Dmg1, Modification.Water);
            var card2 = new Spell(Guid.NewGuid(), Dmg2, Modification.Normal);

            // act
            new WaterRule().CalculateDamage(card1, card2);

            // assert
            Assert.AreEqual(Dmg1 / 2, card1.Damage);
        }

        /// <summary>
        /// Checks if water damage is doubled if other card is fire
        /// </summary>
        /// <param name="Dmg1">damage of card 1</param>
        /// <param name="Dmg2">damage of card 2</param>
        [TestCase(10, 1)]
        [TestCase(20, 100)]
        [TestCase(100, 51)]
        public void CheckIfWaterDamageIsDoubledWhenOtherCardHasFireMod(int Dmg1, int Dmg2)
        {
            // arrange
            var card1 = new Spell(Guid.NewGuid(), Dmg1, Modification.Water);
            var card2 = new Spell(Guid.NewGuid(), Dmg2, Modification.Fire);

            // act
            new WaterRule().CalculateDamage(card1, card2);

            // assert
            Assert.AreEqual(Dmg1 * 2, card1.Damage);
        }

        /// <summary>
        /// Checks if water damage is same if other card is water
        /// </summary>
        /// <param name="Dmg1">damage of card 1</param>
        /// <param name="Dmg2">damage of card 2</param>
        [TestCase(10, 1)]
        [TestCase(20, 100)]
        [TestCase(100, 51)]
        public void CheckIfWaterDamageIsSameWhenOtherCardHasWaterMod(int Dmg1, int Dmg2)
        {
            // arrange
            var card1 = new Spell(Guid.NewGuid(), Dmg1, Modification.Water);
            var card2 = new Spell(Guid.NewGuid(), Dmg2, Modification.Water);

            // act
            new WaterRule().CalculateDamage(card1, card2);

            // assert
            Assert.AreEqual(Dmg1, card1.Damage);
        }
    }
}