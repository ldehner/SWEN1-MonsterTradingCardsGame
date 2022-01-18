using System.Net;
using MonsterTradingCardsGameServer.Core.Routing;
using MonsterTradingCardsGameServer.Core.Server;
using MonsterTradingCardsGameServer.DAL;
using MonsterTradingCardsGameServer.Manager;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing
{
    /// <summary>
    ///     Sets up the repositorys and managers
    /// </summary>
    public class MtcgDbSetup
    {
        private readonly HttpServer _httpServer;

        public MtcgDbSetup()
        {
            var userRepository = new InDatabaseUserRepository();
            var battleRepository = new InDatabaseBattleRepository();
            var cardRepository = new InDatabaseCardRepository();
            var tradeRepository = new InDatabaseTradeRepository(cardRepository);
            var packageRepository = new InDatabasePackageRepository(cardRepository);

            var userManager = new UserManager(userRepository);
            var battleManager = new BattleManager(userManager, battleRepository);
            var cardManager = new CardManager(cardRepository);
            var tradeManager = new TradeManager(tradeRepository, userManager, cardManager);
            var packageManager = new PackageManager(packageRepository);

            var identityProvider = new UserIdentityProvider(userRepository);
            var routeParser = new AppendixRouteParser();
            var router = new Router(routeParser, identityProvider);

            Routes.RegisterUserRoutes(router, userManager);
            Routes.RegisterBattleRoutes(router, battleManager);
            Routes.RegisterCardRoutes(router, cardManager);
            Routes.RegisterTradeRoutes(router, tradeManager);
            Routes.RegisterPackageRoutes(router, packageManager, userManager);

            _httpServer = new HttpServer(IPAddress.Any, 10001, router);
        }

        /// <summary>
        ///     Starts the server
        /// </summary>
        public void Start()
        {
            _httpServer.Start();
        }
    }
}