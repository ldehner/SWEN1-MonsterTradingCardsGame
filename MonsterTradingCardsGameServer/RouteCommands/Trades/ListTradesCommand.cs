using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.RouteCommands.Trades
{
    public class ListTradesCommand : ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;

        public ListTradesCommand(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public override Response Execute()
        {
            var response = new Response();
            var result = _userManager.ListTrades();
            response.Payload = JsonConvert.SerializeObject(result);
            response.StatusCode = StatusCode.Ok;
            return response;
        }
    }
}