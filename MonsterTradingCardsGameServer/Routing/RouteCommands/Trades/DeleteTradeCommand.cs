using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Trades
{
    public class DeleteTradeCommand : ProtectedRouteCommand
    {
        private readonly string _tradeId;
        private readonly IUserManager _userManager;

        public DeleteTradeCommand(IUserManager userManager, string tradeId)
        {
            _userManager = userManager;
            _tradeId = tradeId;
        }

        public override Response Execute()
        {
            return _userManager.DeleteTrade(User.Username, _tradeId)
                ? new Response {StatusCode = StatusCode.Ok}
                : new Response {StatusCode = StatusCode.Unauthorized};
        }
    }
}