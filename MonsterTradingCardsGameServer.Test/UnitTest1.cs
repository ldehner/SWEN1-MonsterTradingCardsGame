using MonsterTradingCardsGameServer.Cards;
using NUnit.Framework;

namespace MonsterTradingCardsGameServer.Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestMonsterDamage()
        {
            var monster = new Monster(10, Modification.Fire, MonsterType.Dragon);
            Assert.AreEqual(10, monster.Damage);
        }
        
        [Test]
        public void TestMonsterDamage2()
        {
            var monster = new Monster(10, Modification.Fire, MonsterType.Dragon);
            Assert.AreEqual(11, monster.Damage);
        }

    }
}