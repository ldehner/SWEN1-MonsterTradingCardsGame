using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Trades;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Trades
{
    public class CreateTradeCommand : ProtectedRouteCommand
    {
        private readonly UserRequestTrade _userRequestTrade;
        private readonly IUserManager _userManager;

        public CreateTradeCommand(IUserManager userManager, UserRequestTrade userRequestTrade)
        {
            _userManager = userManager;
            _userRequestTrade = userRequestTrade;
        }

        public override Response Execute()
        {
            return _userManager.CreateTrade(User.Username, _userRequestTrade)
                ? new Response {StatusCode = StatusCode.Created}
                : new Response {StatusCode = StatusCode.Conflict};
        }
    }
}