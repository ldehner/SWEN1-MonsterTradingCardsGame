using System;
using MonsterTradingCardsGameServer.Cards;
using NUnit.Framework;

namespace MonsterTradingCardsGameServer.Test
{
    /// <summary>
    /// Checks if all card convertions work
    /// </summary>
    [TestFixture]
    public class CardConvertionTest
    {
        /// <summary>
        /// Checks if spell card is converted successfully to universal card
        /// </summary>
        /// <param name="damage">damage of the card</param>
        /// <param name="mod">modification of the card</param>
        [TestCase(100.5, Modification.Fire)]
        [TestCase(12.2, Modification.Water)]
        [TestCase(54.9, Modification.Normal)]
        public void CheckIfSpellCardIsConvertedSuccessfulToUniversalCard(double damage, Modification mod)
        {
            // arrange
            var card = new Spell(Guid.NewGuid(), damage, mod);
            
            // act
            var universalCard = card.ToUniversalCard();
            
            // assert
            Assert.AreEqual(card.Id, universalCard.Id);
            Assert.AreEqual(damage, universalCard.Damage);
            Assert.AreEqual(mod, universalCard.Modification);
            Assert.AreEqual(MonsterType.None, universalCard.MonsterType);
        }
        
        /// <summary>
        /// Checks if monster card is converted successfully to universal card
        /// </summary>
        /// <param name="damage">damage of the card</param>
        /// <param name="mod">modification of the card</param>
        /// <param name="type">type of the card</param>
        [TestCase(100.5, Modification.Fire, MonsterType.Dragon)]
        [TestCase(34.4, Modification.Water, MonsterType.Wizard)]
        [TestCase(65.6, Modification.Normal, MonsterType.Goblin)]
        [TestCase(32.0, Modification.Fire, MonsterType.Elve)]
        [TestCase(43.1, Modification.Water, MonsterType.Knight)]
        [TestCase(87.2, Modification.Normal, MonsterType.Kraken)]
        [TestCase(73.9, Modification.Fire, MonsterType.Org)]
        public void CheckIfMonsterCardIsConvertedSuccessfulToUniversalCard(double damage, Modification mod, MonsterType type)
        {
            // arrange
            var card = new Monster(Guid.NewGuid(), damage, mod, type);
            
            // act
            var universalCard = card.ToUniversalCard();
            
            // assert
            Assert.AreEqual(card.Id, universalCard.Id);
            Assert.AreEqual(damage, universalCard.Damage);
            Assert.AreEqual(mod, universalCard.Modification);
            Assert.AreEqual(type, universalCard.MonsterType);
        }
        
        /// <summary>
        /// Checks if spell card is converted successfully to readable card
        /// </summary>
        /// <param name="damage">damage of the card</param>
        /// <param name="mod">modification of the card</param>
        [TestCase(100.5, Modification.Fire)]
        [TestCase(12.2, Modification.Water)]
        [TestCase(54.9, Modification.Normal)]
        public void CheckIfSpellCardIsConvertedSuccessfulToReadableCard(double damage, Modification mod)
        {
            // arrange
            var card = new Spell(Guid.NewGuid(), damage, mod);
            
            // act
            var readableCard = card.ToReadableCard();
            
            // assert
            Assert.AreEqual(card.Id.ToString(), readableCard.Id);
            Assert.AreEqual(damage, readableCard.Damage);
            Assert.AreEqual(card.GetCardName(), readableCard.CardName);
        }
        
        /// <summary>
        /// Checks if monster card is converted successfully to readable card
        /// </summary>
        /// <param name="damage">damage of the card</param>
        /// <param name="mod">modification of the card</param>
        /// <param name="type">monster type of the card</param>
        [TestCase(100.5, Modification.Fire, MonsterType.Dragon)]
        [TestCase(34.4, Modification.Water, MonsterType.Wizard)]
        [TestCase(65.6, Modification.Normal, MonsterType.Goblin)]
        [TestCase(32.0, Modification.Fire, MonsterType.Elve)]
        [TestCase(43.1, Modification.Water, MonsterType.Knight)]
        [TestCase(87.2, Modification.Normal, MonsterType.Kraken)]
        [TestCase(73.9, Modification.Fire, MonsterType.Org)]
        public void CheckIfMonsterCardIsConvertedSuccessfulToReadableCard(double damage, Modification mod, MonsterType type)
        {
            // arrange
            var card = new Monster(Guid.NewGuid(), damage, mod, type);
            
            // act
            var readableCard = card.ToReadableCard();
            
            // assert
            Assert.AreEqual(card.Id.ToString(), readableCard.Id);
            Assert.AreEqual(card.Damage, readableCard.Damage);
            Assert.AreEqual(card.GetCardName(), readableCard.CardName);
        }
        
        /// <summary>
        /// Checks if universal card is converted successfully to spell card
        /// </summary>
        /// <param name="damage">damage of the card</param>
        /// <param name="mod">modification of the card</param>
        [TestCase(100.5, Modification.Fire)]
        [TestCase(12.2, Modification.Water)]
        [TestCase(54.9, Modification.Normal)]
        public void CheckIfUniversalCardIsConvertedSuccessfulToSpellCard(double damage, Modification mod)
        {
            // arrange
            var universalCard = new UniversalCard(Guid.NewGuid().ToString(), mod, MonsterType.None, damage);
            
            // act
            var spell = universalCard.ToCard();

            // assert
            Assert.AreEqual(universalCard.Id, spell.Id);
            Assert.AreEqual(universalCard.Damage, spell.Damage);
            Assert.AreEqual(universalCard.Modification, spell.Mod);

        }
        
        /// <summary>
        /// checks if universal card is converted successfully to monster card
        /// </summary>
        /// <param name="damage">damage of the card</param>
        /// <param name="mod">modification of the card</param>
        /// <param name="type">monster type of the card</param>
        [TestCase(100.5, Modification.Fire, MonsterType.Dragon)]
        [TestCase(34.4, Modification.Water, MonsterType.Wizard)]
        [TestCase(65.6, Modification.Normal, MonsterType.Goblin)]
        [TestCase(32.0, Modification.Fire, MonsterType.Elve)]
        [TestCase(43.1, Modification.Water, MonsterType.Knight)]
        [TestCase(87.2, Modification.Normal, MonsterType.Kraken)]
        [TestCase(73.9, Modification.Fire, MonsterType.Org)]
        public void CheckIfUniversalCardIsConvertedSuccessfulToMonsterCard(double damage, Modification mod, MonsterType type)
        {
            // arrange
            var universalCard = new UniversalCard(Guid.NewGuid().ToString(), mod, type, damage);
            
            // act
            var monster = universalCard.ToCard();

            // assert
            Assert.AreEqual(universalCard.Id, monster.Id);
            Assert.AreEqual(universalCard.Damage, monster.Damage);
            Assert.AreEqual(universalCard.Modification, monster.Mod);
            Assert.AreEqual(universalCard.MonsterType, ((Monster) monster).Type);
        }
        
        /// <summary>
        /// checks if user request card is converted successfully to universal card
        /// </summary>
        /// <param name="name">name of the card</param>
        /// <param name="damage">damage of the card</param>
        /// <param name="mod">modification of the card</param>
        /// <param name="type">monster type of the card</param>
        [TestCase("RegularSpell", 100.5, Modification.Normal, MonsterType.None)]
        [TestCase("FireSpell", 32.7, Modification.Fire, MonsterType.None)]
        [TestCase("WaterSpell", 57.1, Modification.Water, MonsterType.None)]
        [TestCase("Knight", 100.5, Modification.Normal, MonsterType.Knight)]
        [TestCase("Org", 100.5, Modification.Normal, MonsterType.Org)]
        [TestCase("FireKraken", 100.5, Modification.Fire, MonsterType.Kraken)]
        [TestCase("WaterWizard", 33.5, Modification.Water, MonsterType.Wizard)]
        [TestCase("WaterGoblin", 33.5, Modification.Water, MonsterType.Goblin)]
        [TestCase("Dragon", 33.5, Modification.Normal, MonsterType.Dragon)]
        [TestCase("FireElve", 33.5, Modification.Fire, MonsterType.Elve)]
        public void CheckIfUserRequestCardIsConvertedSuccessfulToUniversalCard(string name, double damage, Modification mod, MonsterType type)
        {
            // arrange
            var id = Guid.NewGuid().ToString();
            var userRequestCard = new UserRequestCard(id, name, damage);

            // act
            var card = userRequestCard.ToUniversalCard();

            //assert
            Assert.AreEqual(id, card.Id.ToString());
            Assert.AreEqual(mod, card.Modification);
            Assert.AreEqual(type, card.MonsterType);
            Assert.AreEqual(damage, card.Damage);
        }
        
        /// <summary>
        /// Checks if Exception is thrown if card was not found
        /// </summary>
        /// <param name="name">name of the card</param>
        /// <param name="damage">damage of the card</param>
        [TestCase("WeirdSpell", 100.5)]
        [TestCase("NoSpell", 100.5)]
        [TestCase("NormalBanana", 22.5)]
        [TestCase("FireRat", 4.5)]
        [TestCase("WaterFrog", 12.3)]
        public void CheckIfUserRequestCardIsConvertedSuccessfulToUniversalCard(string name, double damage)
        {
            // arrange
            var id = Guid.NewGuid().ToString();
            var userRequestCard = new UserRequestCard(id, name, damage);

            // act
            //assert
            Assert.Throws<CardNotFoundException>(() => userRequestCard.ToUniversalCard());
        }
        
        
    }
}