using System.Net;
using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Core.Routing;
using MonsterTradingCardsGameServer.Core.Server;
using MonsterTradingCardsGameServer.DAL;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing
{
    /// <summary>
    /// Sets up the repositorys and managers
    /// </summary>
    public class MtcgDbSetup
    {
        private HttpServer _httpServer;
        public MtcgDbSetup()
        {
            var userRepository = new InDatabaseUserRepository();
            var battleRepository = new InDatabaseBattleRepository();
            var userManager = new UserManager(userRepository);
            var battleManager = new BattleManager(userManager, battleRepository);

            var identityProvider = new UserIdentityProvider(userRepository);
            var routeParser = new AppendixRouteParser();

            var router = new Router(routeParser, identityProvider);
            Routes.RegisterUserRoutes(router, userManager);
            Routes.RegisterBattleRoutes(router, battleManager);

            _httpServer = new HttpServer(IPAddress.Any, 10001, router);
        }
        
        /// <summary>
        /// Starts the server
        /// </summary>
        public void Start()
        {
            _httpServer.Start();
        }
    }
}