using System;
using System.Collections.Generic;
using MonsterTradingCardsGame.App.Battles;
using MonsterTradingCardsGame.App.Cards;
using MonsterTradingCardsGame.App.Rules;
using MonsterTradingCardsGame.App.Users;

namespace MonsterTradingCardsGame.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // var manager = new UserManager();
            // manager.LoginUser("bla", "123");
            // manager.RegisterUser("bla", "123", "fosdfojds");
            // Console.WriteLine("Hello World!");
            /*Card card1 = new Spell(10, Modification.Normal);
            Card card2 = new Spell(10, Modification.Water);
            Card card3 = new Monster(20, Modification.Fire, MonsterType.Knight);
            Rule rule = new FireRule();
            Rule rule2 = new KnightWaterRule();
            rule.CalculateDamage(card3, card1);
            rule2.CalculateDamage(card3, card2);
            Console.WriteLine(card1.Damage);
            Console.WriteLine(card3.Damage);*/
            
            /*Console.WriteLine(card.GetCardName());
            Card card2 = new Spell(10, Modification.Water);
            Console.WriteLine(card.GetType().IsInstanceOfType(new Monster(11,Modification.Water, MonsterType.Goblin)));
            Console.WriteLine(card.GetType().IsInstanceOfType(new Spell(11,Modification.Water)));
            Console.WriteLine(card2.GetType().IsInstanceOfType(new Spell(11,Modification.Water)));
            Console.WriteLine(((Monster) card).Type);*/

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
            return new User("test 1", 40, stats, "bio", new Stack(new List<Card>(cards1)), new Deck(new List<Card>(cards1)));
        }
        
    }
}
