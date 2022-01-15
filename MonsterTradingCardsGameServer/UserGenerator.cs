using System;
using System.Collections.Generic;
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
                    tempCard = new Spell(Guid.NewGuid(), rand.Next(20),
                        modifications[rand.Next(2, modifications.Count)]);
                else
                    tempCard = new Monster(Guid.NewGuid(), rand.Next(20),
                        modifications[rand.Next(2, modifications.Count)],
                        monsterTypes[rand.Next(2, monsterTypes.Count)]);
                cards1.Add(tempCard);
            }

            var stats = new[] {1, 1};
            var user = new User(username, new Stats(0, 0), new UserData(username, "Coole Bio", ":-)"),
                new Stack(new List<Card>(cards1)), new Deck(new List<Card>(cards1)), 20);
            Console.WriteLine(JsonConvert.SerializeObject(user.Stack));
            return user;
        }
    }
}