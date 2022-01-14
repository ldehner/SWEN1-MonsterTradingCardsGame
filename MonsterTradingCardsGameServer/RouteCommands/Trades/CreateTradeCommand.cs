using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.RouteCommands.Trades
{
    public class CreateTradeCommand : ProtectedRouteCommand
    {
        private IUserManager _userManager;
        private User _currentUser;
        private TradingDeal _tradingDeal;
        public CreateTradeCommand(IUserManager userManager, User currentUser, TradingDeal tradingDeal)
        {
            _userManager = userManager;
            _currentUser = currentUser;
            _tradingDeal = tradingDeal;
        }
        
        public override Response Execute()
        {
            return _userManager.CreateTrade(_currentUser.Username, _tradingDeal) ? new Response() {StatusCode = StatusCode.Created} : new Response() {StatusCode = StatusCode.Conflict};
        }
    }
}