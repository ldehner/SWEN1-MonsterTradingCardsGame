using MonsterTradingCardsGameServer.Routing;

namespace MonsterTradingCardsGameServer
{
    /// <summary>
    ///     Creates Repositorys, Router and starts the Server
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            var mtcg = new MtcgDbSetup();
            mtcg.Start();
        }
    }
}