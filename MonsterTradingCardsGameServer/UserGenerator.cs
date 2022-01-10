using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer
{
    public static class UserGenerator
    {
        public static User GenerateUser(string username)
        {
            var monsterTypes = new List<MonsterType>();
            var modifications = new List<Modification>();

            monsterTypes.Add(MonsterType.Dragon);
            monsterTypes.Add(MonsterType.Goblin);
            monsterTypes.Add(MonsterType.Org);
            monsterTypes.Add(MonsterType.Knight);
            monsterTypes.Add(MonsterType.Kraken);
            monsterTypes.Add(MonsterType.Elve);
            
            modifications.Add(Modification.Fire);
            modifications.Add(Modification.Water);
            modifications.Add(Modification.Normal);
            
            var rand = new Random();

            var cards1 = new List<Card>();
            for (var i = 0; i < 4; i++)
            {
                Card tempCard;
                if (rand.Next(10) > 5)
                {
                    tempCard = new Spell(rand.Next(20), modifications[rand.Next(modifications.Count)]);
                }
                else
                {
                    tempCard = new Monster(rand.Next(20), modifications[rand.Next(modifications.Count)], monsterTypes[rand.Next(monsterTypes.Count)]);
                }
                (new CardRuleAdder(tempCard)).AddRules();
                cards1.Add(tempCard);

            }
            var stats = new int[] {1, 1};
            var user = new User(username, new Stats(0, 0), new UserData(username, "Coole Bio", ":-)"),
                new Stack(new List<Card>(cards1)), new Deck(new List<Card>(cards1)), 20);
            Console.WriteLine(JsonConvert.SerializeObject(user.Stack));
            return user;
        }
    }
}