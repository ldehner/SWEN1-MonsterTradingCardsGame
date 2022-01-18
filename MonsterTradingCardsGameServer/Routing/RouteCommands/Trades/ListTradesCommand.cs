using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Manager;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Trades
{
    /// <summary>
    ///     Lists all available trades
    /// </summary>
    public class ListTradesCommand : ProtectedRouteCommand
    {
        private readonly ITradeManager _tradeManager;

        /// <summary>
        ///     Sets user manager
        /// </summary>
        /// <param name="tradeManager">the trade manager</param>
        public ListTradesCommand(ITradeManager tradeManager)
        {
            _tradeManager = tradeManager;
        }

        /// <summary>
        ///     Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            var response = new Response();
            var result = _tradeManager.ListTrades();
            response.Payload = JsonConvert.SerializeObject(result);
            response.StatusCode = StatusCode.Ok;
            return response;
        }
    }
}