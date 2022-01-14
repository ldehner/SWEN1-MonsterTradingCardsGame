using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.RouteCommands.Trades
{
    public class DeleteTradeCommand : ProtectedRouteCommand
    {
        private IUserManager _userManager;
        private User _currentUser;
        private string _tradeId;
        public DeleteTradeCommand(IUserManager userManager, User currentUser, string tradeId)
        {
            _userManager = userManager;
            _currentUser = currentUser;
            _tradeId = tradeId;
        }
        public override Response Execute()
        {
            return _userManager.DeleteTrade(_currentUser.Username, _tradeId)
                ? new Response() {StatusCode = StatusCode.Ok}
                : new Response() {StatusCode = StatusCode.Unauthorized};
        }
    }
}