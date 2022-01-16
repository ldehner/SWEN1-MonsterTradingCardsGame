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
    /// <summary>
    /// Creates Repositorys, Router and starts the Server
    /// </summary>
    internal class Program
    {
        private static IIdentityProvider _identityProvider;

        private static void Main(string[] args)
        {
            var userRepository =
                new InDatabaseUserRepository();
            var battleRepository = new InDatabaseBattleRepository();
            var userManager = new UserManager(userRepository);
            var battleManager = new BattleManager(userManager, battleRepository);

            _identityProvider = new UserIdentityProvider(userRepository);
            var routeParser = new AppendixRouteParser();

            var router = new Router(routeParser, _identityProvider);
            RegisterUserRoutes(router, userManager);
            RegisterBattleRoutes(router, battleManager);

            var httpServer = new HttpServer(IPAddress.Any, 10001, router);
            httpServer.Start();
        }

        private static void RegisterUserRoutes(Router router, IUserManager userManager)
        {
            // public routes
            router.AddRoute(HttpMethod.Post, "/sessions",
                (r, p) => new LoginCommand(userManager, Deserialize<Credentials>(r.Payload)));
            router.AddRoute(HttpMethod.Post, "/users",
                (r, p) => new RegisterCommand(userManager, Deserialize<Credentials>(r.Payload)));

            // protected routes
            router.AddProtectedRoute(HttpMethod.Get, "/users/{appendix}",
                (r, p) => new ListBioCommand(userManager, p["appendix"]));
            router.AddProtectedRoute(HttpMethod.Put, "/users/{appendix}",
                (r, p) => new EditBioCommand(userManager, p["appendix"], GetUserIdentity(r),
                    Deserialize<UserData>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Get, "/stats",
                (r, p) => new GetStatsCommand(userManager, GetUserIdentity(r)));
            router.AddProtectedRoute(HttpMethod.Get, "/score", (r, p) => new GetScoreBoardCommand(userManager));
            router.AddProtectedRoute(HttpMethod.Get, "/cards",
                (r, p) => new GetStackCommand(userManager, GetUserIdentity(r)));
            router.AddProtectedRoute(HttpMethod.Get, "/deck",
                (r, p) => new GetDeckCommand(userManager, GetUserIdentity(r)));
            router.AddProtectedRoute(HttpMethod.Put, "/deck",
                (r, p) => new SetDeckCommand(userManager, GetUserIdentity(r), r.Payload));
            router.AddProtectedRoute(HttpMethod.Post, "/packages",
                (r, p) => new AddPackageCommand(userManager, GetUserIdentity(r),
                    Deserialize<List<UserRequestCard>>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Get, "/packages",
                (r, p) => new AquirePackageCommand(userManager, GetUserIdentity(r)));
            router.AddProtectedRoute(HttpMethod.Post, "/tradings",
                (r, p) => new CreateTradeCommand(userManager, GetUserIdentity(r), Deserialize<UserRequestTrade>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Get, "/tradings", (r, p) => new ListTradesCommand(userManager));
            router.AddProtectedRoute(HttpMethod.Post, "/tradings/{appendix}",
                (r, p) => new AcceptTradeCommand(userManager, GetUserIdentity(r), p["appendix"],
                    Deserialize<string>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Delete, "/tradings/{appendix}",
                (r, p) => new DeleteTradeCommand(userManager, GetUserIdentity(r), p["appendix"]));
            router.AddProtectedRoute(HttpMethod.Post, "/logout",
                (r, p) => new LogoutUserCommand(userManager, GetUserIdentity(r)));
        }

        private static void RegisterBattleRoutes(Router router, IBattleManager battleManager)
        {
            router.AddProtectedRoute(HttpMethod.Post, "/battles",
                (r, p) => new StartBattleCommand(battleManager, GetUserIdentity(r)));
            router.AddProtectedRoute(HttpMethod.Get, "/battles",
                (r, p) => new ListBattlesCommand(battleManager, GetUserIdentity(r)));
            router.AddProtectedRoute(HttpMethod.Get, "/battles/{appendix}",
                (r, p) => new GetBattleCommand(battleManager, GetUserIdentity(r), p["appendix"]));
        }

        private static T Deserialize<T>(string payload) where T : class
        {
            var deserializedData = JsonConvert.DeserializeObject<T>(payload);
            return deserializedData;
        }

        private static User GetUserIdentity(RequestContext request)
        {
            return (User) _identityProvider.GetIdentityForRequest(request);
        }
    }
}