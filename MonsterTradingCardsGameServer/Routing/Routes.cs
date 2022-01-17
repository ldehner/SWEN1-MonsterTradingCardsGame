using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Core.Request;
using MonsterTradingCardsGameServer.Core.Routing;
using MonsterTradingCardsGameServer.Manager;
using MonsterTradingCardsGameServer.Routing.RouteCommands.Admin;
using MonsterTradingCardsGameServer.Routing.RouteCommands.Battles;
using MonsterTradingCardsGameServer.Routing.RouteCommands.Cards;
using MonsterTradingCardsGameServer.Routing.RouteCommands.Trades;
using MonsterTradingCardsGameServer.Routing.RouteCommands.Users;
using MonsterTradingCardsGameServer.Trades;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing
{
    /// <summary>
    /// Sets all routes
    /// </summary>
    public static class Routes
    {
        /// <summary>
        /// Registers user specific routes
        /// </summary>
        /// <param name="router">the router</param>
        /// <param name="userManager">the user manager</param>
        public static void RegisterUserRoutes(Router router, IUserManager userManager)
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
                (r, p) => new EditBioCommand(userManager, p["appendix"],
                    Deserialize<UserData>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Get, "/stats",
                (r, p) => new GetStatsCommand(userManager));
            router.AddProtectedRoute(HttpMethod.Get, "/score", (r, p) => new GetScoreBoardCommand(userManager));
            router.AddProtectedRoute(HttpMethod.Post, "/logout",
                (r, p) => new LogoutUserCommand(userManager));
        }

        /// <summary>
        /// Registers card specific routes
        /// </summary>
        /// <param name="router">router</param>
        /// <param name="cardManager">card manager</param>
        public static void RegisterCardRoutes(Router router, ICardManager cardManager)
        {
            router.AddProtectedRoute(HttpMethod.Get, "/cards",
                (r, p) => new GetStackCommand(cardManager));
            router.AddProtectedRoute(HttpMethod.Get, "/deck",
                (r, p) => new GetDeckCommand(cardManager));
            router.AddProtectedRoute(HttpMethod.Get, "/deck{appendix}",
                (r, p) => new GetPlainDeckCommand(cardManager, p["appendix"]));
            router.AddProtectedRoute(HttpMethod.Put, "/deck",
                (r, p) => new SetDeckCommand(cardManager, Deserialize<List<string>>(r.Payload)));
        }
        
        /// <summary>
        /// Registers trade specific routes
        /// </summary>
        /// <param name="router">router</param>
        /// <param name="tradeManager">trade manager</param>
        public static void RegisterTradeRoutes(Router router, ITradeManager tradeManager)
        {
            router.AddProtectedRoute(HttpMethod.Post, "/tradings",
                (r, p) => new CreateTradeCommand(tradeManager, Deserialize<UserRequestTrade>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Get, "/tradings", (r, p) => new ListTradesCommand(tradeManager));
            router.AddProtectedRoute(HttpMethod.Post, "/tradings/{appendix}",
                (r, p) => new AcceptTradeCommand(tradeManager, p["appendix"],
                    Deserialize<string>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Delete, "/tradings/{appendix}",
                (r, p) => new DeleteTradeCommand(tradeManager, p["appendix"]));
        }
        
        /// <summary>
        /// Registers package specific routes
        /// </summary>
        /// <param name="router">router</param>
        /// <param name="packageManager">package manager</param>
        public static void RegisterPackageRoutes(Router router, IPackageManager packageManager)
        {
            router.AddProtectedRoute(HttpMethod.Post, "/packages",
                (r, p) => new AddPackageCommand(packageManager,
                    Deserialize<List<UserRequestCard>>(r.Payload)));
            router.AddProtectedRoute(HttpMethod.Get, "/packages",
                (r, p) => new AquirePackageCommand(packageManager));
        }

        /// <summary>
        /// Registers Battle Routes
        /// </summary>
        /// <param name="router">Router</param>
        /// <param name="battleManager">Battle Manager</param>
        public static void RegisterBattleRoutes(Router router, IBattleManager battleManager)
        {
            router.AddProtectedRoute(HttpMethod.Post, "/battles",
                (r, p) => new StartBattleCommand(battleManager));
            router.AddProtectedRoute(HttpMethod.Get, "/battles",
                (r, p) => new ListBattlesCommand(battleManager));
            router.AddProtectedRoute(HttpMethod.Get, "/battles/{appendix}",
                (r, p) => new GetBattleCommand(battleManager, p["appendix"]));
        }
        
        /// <summary>
        /// Deserializes the payload
        /// </summary>
        /// <param name="payload">the payload</param>
        /// <typeparam name="T">object type</typeparam>
        /// <returns>the deserialized object</returns>
        private static T Deserialize<T>(string payload) where T : class
        {
            var deserializedData = JsonConvert.DeserializeObject<T>(payload);
            return deserializedData;
        }
    }
}