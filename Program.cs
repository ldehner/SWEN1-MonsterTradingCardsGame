using System;
using System.Collections.Generic;

namespace MonsterTradingCardsGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // var manager = new UserManager();
            // manager.LoginUser("bla", "123");
            // manager.RegisterUser("bla", "123", "fosdfojds");
            // Console.WriteLine("Hello World!");
            Card card = new Monster(10,Modification.Fire, MonsterType.Dragon);
            Console.WriteLine(card.GetCardName());
            Card card2 = new Spell(10, Modification.Water);
            Console.WriteLine(card.GetType().IsInstanceOfType(new Monster(11,Modification.Water, MonsterType.Goblin)));
            Console.WriteLine(card.GetType().IsInstanceOfType(new Spell(11,Modification.Water)));
            Console.WriteLine(card2.GetType().IsInstanceOfType(new Spell(11,Modification.Water)));
            Console.WriteLine(((Monster) card).Type);

            var manager = new UserManager();
            var battle_manager = new BattleManager();

            var token1 = manager.TempLogin(GenerateUser());
            var token2 = manager.TempLogin(GenerateUser());
            
            battle_manager.NewBattle(manager.users[token1]);
            battle_manager.NewBattle(manager.users[token2]);
            
            Console.WriteLine(manager.users[token1].Wins+" "+manager.users[token1].Losses);
            Console.WriteLine(manager.users[token2].Wins+" "+manager.users[token2].Losses);
            
            
        }

        static User GenerateUser()
        {
            var monsterTypes = new List<MonsterType>();
            var modifications = new List<Modification>();

            int lvl;
            
            
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
            for (int i = 0; i < 4; i++)
            {
                if (rand.Next(10) > 5)
                {
                    cards1.Add(new Spell(rand.Next(20), modifications[rand.Next(modifications.Count)]));
                }
                else
                {
                    cards1.Add(new Monster(rand.Next(20), modifications[rand.Next(modifications.Count)], monsterTypes[rand.Next(monsterTypes.Count)]));
                }
            }

            var stats = new int[] {1, 1};
            return new User("test 1", 40, stats, "bio", new Stack(new List<Card>(cards1)), new Deck(new List<Card>(cards1)));
        }
        
    }
}
