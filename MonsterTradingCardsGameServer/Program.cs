using System;
using System.Collections.Generic;
using System.Net;
using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Core.Authentication;
using MonsterTradingCardsGameServer.Core.Request;
using MonsterTradingCardsGameServer.Core.Routing;
using MonsterTradingCardsGameServer.Core.Server;
using MonsterTradingCardsGameServer.DAL;
using MonsterTradingCardsGameServer.RouteCommands.Admin;
using MonsterTradingCardsGameServer.RouteCommands.Battles;
using MonsterTradingCardsGameServer.RouteCommands.Cards;
using MonsterTradingCardsGameServer.RouteCommands.Trades;
using MonsterTradingCardsGameServer.RouteCommands.Users;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer
{
    class Program
    {
        public static IIdentityProvider identityProvider;
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

            /**
            RequestToCardConverter.ConvertToUniversalCard("02a9c76e-b17d-427f-9240-2dd49b0d3bfd", 10, "RegularSpell");
            RequestToCardConverter.ConvertToUniversalCard("02a9c76e-b17d-427f-9240-2dd49b0d3bfd", 10, "WaterSpell");
            RequestToCardConverter.ConvertToUniversalCard("02a9c76e-b17d-427f-9240-2dd49b0d3bfd", 10, "FireDragon");
            RequestToCardConverter.ConvertToUniversalCard("02a9c76e-b17d-427f-9240-2dd49b0d3bfd", 10, "Elve");
            RequestToCardConverter.ConvertToUniversalCard("02a9c76e-b17d-427f-9240-2dd49b0d3bfd", 10, "WaterGoblin");
**/
            var userRepository =
                new InDatabaseUserRepository();
            var battleRepository = new InDatabaseBattleRepository();
            var userManager = new UserManager(userRepository);
            var battleManager = new BattleManager(userManager, battleRepository);
            
            identityProvider = new UserIdentityProvider(userRepository);
            var routeParser = new AppendixRouteParser();

            var router = new Router(routeParser, identityProvider);
            RegisterUserRoutes(router, userManager);
            RegisterBattleRoutes(router, battleManager);
            
            var httpServer = new HttpServer(IPAddress.Any, 10001, router);
            httpServer.Start();
   


        }
        private static void RegisterUserRoutes(Router router, IUserManager userManager)
        {
            // public routes
            router.AddRoute(HttpMethod.Post, "/sessions", (r, p) => new LoginCommand(userManager, Deserialize<Credentials>(r.Payload)));
            router.AddRoute(HttpMethod.Post, "/users", (r, p) => new RegisterCommand(userManager, Deserialize<Credentials>(r.Payload)));

            // protected routes
            router.AddProtectedRoute(HttpMethod.Get, "/users/{appendix}", (r, p) => new ListBioCommand(userManager, p["appendix"]));
            router.AddProtectedRoute(HttpMethod.Put, "/users/{appendix}", (r, p) => new EditBioCommand(userManager, p["appendix"], GetUserIdentity(r), Deserialize<UserData>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Get, "/stats", (r, p) => new GetStatsCommand(userManager, GetUserIdentity(r)));
            router.AddProtectedRoute(HttpMethod.Get, "/score", (r, p) => new GetScoreBoardCommand(userManager));
            router.AddProtectedRoute(HttpMethod.Get, "/cards", (r, p) => new GetStackCommand(userManager, GetUserIdentity(r)));
            router.AddProtectedRoute(HttpMethod.Get, "/deck", (r, p) => new GetDeckCommand(userManager,GetUserIdentity(r)));
            router.AddProtectedRoute(HttpMethod.Put, "/deck", (r, p) => new SetDeckCommand(userManager,GetUserIdentity(r), r.Payload));
            router.AddProtectedRoute(HttpMethod.Post, "/packages", (r, p) => new AddPackageCommand(userManager,GetUserIdentity(r), Deserialize<List<UserRequestCard>>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Get, "/packages", (r, p) => new AquirePackageCommand(userManager,GetUserIdentity(r)));
            router.AddProtectedRoute(HttpMethod.Post, "/tradings", (r, p) => new CreateTradeCommand(userManager, GetUserIdentity(r), Deserialize<TradingDeal>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Get, "/tradings", (r, p) => new ListTradesCommand(userManager));
            router.AddProtectedRoute(HttpMethod.Post, "/tradings/{appendix}", (r, p) => new AcceptTradeCommand(userManager,GetUserIdentity(r), p["appendix"],Deserialize<string>(r.Payload)));
            
            // router.AddProtectedRoute(HttpMethod.Get, "/messages", (r, p) => new ListMessagesCommand(messageManager));
            // router.AddProtectedRoute(HttpMethod.Post, "/messages", (r, p) => new AddMessageCommand(messageManager, r.Payload));
            // router.AddProtectedRoute(HttpMethod.Get, "/messages/{id}", (r, p) => new ShowMessageCommand(messageManager, int.Parse(p["id"])));
            // router.AddProtectedRoute(HttpMethod.Put, "/messages/{id}", (r, p) => new UpdateMessageCommand(messageManager, int.Parse(p["id"]), r.Payload));
            // router.AddProtectedRoute(HttpMethod.Delete, "/messages/{id}", (r, p) => new RemoveMessageCommand(messageManager, int.Parsex(p["id"])));
        }

        private static void RegisterBattleRoutes(Router router, IBattleManager battleManager)
        {
            router.AddProtectedRoute(HttpMethod.Post, "/battles", (r, p) => new StartBattleCommand(battleManager, GetUserIdentity(r)));
        }

        private static T Deserialize<T>(string payload) where T : class
        {
            var deserializedData = JsonConvert.DeserializeObject<T>(payload);
            return deserializedData;
        }

        private static User GetUserIdentity(RequestContext request)
        {
            return (User) identityProvider.GetIdentyForRequest(request);
        }
    }
}
