using System;
using System.Collections.Generic;
using System.Net;
using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Core.Request;
using MonsterTradingCardsGameServer.Core.Routing;
using MonsterTradingCardsGameServer.Core.Server;
using MonsterTradingCardsGameServer.DAL;
using MonsterTradingCardsGameServer.RouteCommands.Users;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer
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

            /**
            var manager = new UserManager();
            var battleManager = new BattleManager();

            var token1 = manager.TempLogin(GenerateUser());
            var token2 = manager.TempLogin(GenerateUser());
            
            battleManager.NewBattle(manager.users[token1]);
            battleManager.NewBattle(manager.users[token2]);
            
            Console.WriteLine(manager.users[token1].Wins+" "+manager.users[token1].Losses);
            Console.WriteLine(manager.users[token2].Wins+" "+manager.users[token2].Losses);
            **/

            var userRepository =
                new InDatabaseUserRepository(
                    "Host=localhost;Username=postgres;Password=mysecretpassword;Database=postgres");
            var userManager = new UserManager(userRepository);
            
            var identityProvider = new UserIdentityProvider(userRepository);
            var routeParser = new UsernameRouteParser();

            var router = new Router(routeParser, identityProvider);
            RegisterRoutes(router, userManager);
            
            var httpServer = new HttpServer(IPAddress.Any, 10001, router);
            httpServer.Start();


        }
        private static void RegisterRoutes(Router router, IUserManager userManager)
        {
            // public routes
            router.AddRoute(HttpMethod.Post, "/sessions", (r, p) => new LoginCommand(userManager, Deserialize<Credentials>(r.Payload)));
            router.AddRoute(HttpMethod.Post, "/users", (r, p) => new RegisterCommand(userManager, Deserialize<Credentials>(r.Payload)));

            // protected routes
            router.AddProtectedRoute(HttpMethod.Get, "/users/{username}", (r, p) => new ListBioCommand(userManager, p["username"]));
            // router.AddProtectedRoute(HttpMethod.Get, "/messages", (r, p) => new ListMessagesCommand(messageManager));
            // router.AddProtectedRoute(HttpMethod.Post, "/messages", (r, p) => new AddMessageCommand(messageManager, r.Payload));
            // router.AddProtectedRoute(HttpMethod.Get, "/messages/{id}", (r, p) => new ShowMessageCommand(messageManager, int.Parse(p["id"])));
            // router.AddProtectedRoute(HttpMethod.Put, "/messages/{id}", (r, p) => new UpdateMessageCommand(messageManager, int.Parse(p["id"]), r.Payload));
            // router.AddProtectedRoute(HttpMethod.Delete, "/messages/{id}", (r, p) => new RemoveMessageCommand(messageManager, int.Parse(p["id"])));
        }

        private static T Deserialize<T>(string payload) where T : class
        {
            var deserializedData = JsonConvert.DeserializeObject<T>(payload);
            return deserializedData;
        }

        private static User GenerateUser()
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
            return new User("test 1", new Stats(0,0), new UserData(20,"Coole Bio"), new Stack(new List<Card>(cards1)), new Deck(new List<Card>(cards1)));
        }
        
    }
}
