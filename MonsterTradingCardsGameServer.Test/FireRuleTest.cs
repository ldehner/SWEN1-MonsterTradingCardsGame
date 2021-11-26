using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Rules;
using NUnit.Framework;

namespace MonsterTradingCardsGameServer.Test
{
    [TestFixture]
    public class FireRuleTest
    {
        [TestCase(10, 1)]
        [TestCase(20,100)]
        [TestCase(100, 51)]
        public void CheckIfFireDamageIsHalvedWhenOtherCardHasWaterMod(int Dmg1, int Dmg2)
        {
            // arrange
            var card1 = new Spell(Dmg1, Modification.Fire);
            var card2 = new Spell(Dmg2, Modification.Water);
            
            // act
            new FireRule().CalculateDamage(card1, card2);
            
            // assert
            Assert.AreEqual(Dmg1/2, card1.Damage);
        }
        
        [TestCase(10, 1)]
        [TestCase(20,100)]
        [TestCase(100, 51)]
        public void CheckIfFireDamageIsDoubledWhenOtherCardHasNormalMod(int Dmg1, int Dmg2)
        {
            // arrange
            var card1 = new Spell(Dmg1, Modification.Fire);
            var card2 = new Spell(Dmg2, Modification.Normal);
            
            // act
            new FireRule().CalculateDamage(card1, card2);
            
            // assert
            Assert.AreEqual(Dmg1*2, card1.Damage);
        }
        
        [TestCase(10, 1)]
        [TestCase(20,100)]
        [TestCase(100, 51)]
        public void CheckIfFireDamageIsSameWhenOtherCardHasFireMod(int Dmg1, int Dmg2)
        {
            // arrange
            var card1 = new Spell(Dmg1, Modification.Fire);
            var card2 = new Spell(Dmg2, Modification.Fire);
            
            // act
            new FireRule().CalculateDamage(card1, card2);
            
            // assert
            Assert.AreEqual(Dmg1, card1.Damage);
        }
    }
}