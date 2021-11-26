using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Rules;
using NUnit.Framework;

namespace MonsterTradingCardsGameServer.Test
{
    [TestFixture]
    public class NormalRuleTest
    {
        [TestCase(10, 1)]
        [TestCase(20,100)]
        [TestCase(100, 51)]
        public void CheckIfNormalDamageIsHalvedWhenOtherCardHasFireMod(int Dmg1, int Dmg2)
        {
            // arrange
            var card1 = new Spell(Dmg1, Modification.Normal);
            var card2 = new Spell(Dmg2, Modification.Fire);
            
            // act
            new NormalRule().CalculateDamage(card1, card2);
            
            // assert
            Assert.AreEqual(Dmg1/2, card1.Damage);
        }
        
        [TestCase(10, 1)]
        [TestCase(20,100)]
        [TestCase(100, 51)]
        public void CheckIfNormalDamageIsDoubledWhenOtherCardHasWaterMod(int Dmg1, int Dmg2)
        {
            // arrange
            var card1 = new Spell(Dmg1, Modification.Normal);
            var card2 = new Spell(Dmg2, Modification.Water);
            
            // act
            new NormalRule().CalculateDamage(card1, card2);
            
            // assert
            Assert.AreEqual(Dmg1*2, card1.Damage);
        }
        
        [TestCase(10, 1)]
        [TestCase(20,100)]
        [TestCase(100, 51)]
        public void CheckIfNormalDamageIsSameWhenOtherCardHasNormalMod(int Dmg1, int Dmg2)
        {
            // arrange
            var card1 = new Spell(Dmg1, Modification.Normal);
            var card2 = new Spell(Dmg2, Modification.Normal);
            
            // act
            new NormalRule().CalculateDamage(card1, card2);
            
            // assert
            Assert.AreEqual(Dmg1, card1.Damage);
        }
    }
}