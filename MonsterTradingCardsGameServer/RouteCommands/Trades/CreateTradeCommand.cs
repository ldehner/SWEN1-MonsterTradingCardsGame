using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.RouteCommands.Trades
{
    public class CreateTradeCommand : ProtectedRouteCommand
    {
        private readonly User _currentUser;
        private readonly UserRequestTrade _userRequestTrade;
        private readonly IUserManager _userManager;

        public CreateTradeCommand(IUserManager userManager, User currentUser, UserRequestTrade userRequestTrade)
        {
            _userManager = userManager;
            _currentUser = currentUser;
            _userRequestTrade = userRequestTrade;
        }

        public override Response Execute()
        {
            return _userManager.CreateTrade(_currentUser.Username, _userRequestTrade)
                ? new Response {StatusCode = StatusCode.Created}
                : new Response {StatusCode = StatusCode.Conflict};
        }
    }
}