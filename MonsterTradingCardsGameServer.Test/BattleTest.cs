using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Users;
using NUnit.Framework;

namespace MonsterTradingCardsGameServer.Test
{
    [TestFixture]
    public class BattleTest
    {
        public void CheckIfBattleOutcomeIsCorrect()
        {
            // arrange
            var d1 = new List<Card>();
            var d2 = new List<Card>();
            d1.Add(new Spell(Guid.NewGuid(), 10, Modification.Normal));
            d1.Add(new Spell(Guid.NewGuid(), 100, Modification.Normal));
            d1.Add(new Spell(Guid.NewGuid(), 20, Modification.Normal));
            d1.Add(new Spell(Guid.NewGuid(), 30, Modification.Normal));
            
            d2.Add(new Spell(Guid.NewGuid(), 10, Modification.Fire));
            d2.Add(new Spell(Guid.NewGuid(), 10, Modification.Fire));
            d2.Add(new Spell(Guid.NewGuid(), 10, Modification.Fire));
            d2.Add(new Spell(Guid.NewGuid(), 10, Modification.Fire));

            var u1 = new User("User1", new Stats(0, 0), new UserData("User 1", "Hey", ":)"), new Stack(d1), new Deck(d1), 20);
            var u2 = new User("User2", new Stats(0, 0), new UserData("User 2", "Hey", ":)"), new Stack(d2), new Deck(d2), 20);

            // act
            var result = new Battle(u1, u2).StartBattle();
            
            // assert
            Assert.AreEqual("User2", result.Winner);
            Assert.AreEqual("User1", result.Loser);
        }
    }
}