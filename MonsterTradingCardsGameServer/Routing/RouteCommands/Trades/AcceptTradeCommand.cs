using System;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Trades
{
    public class AcceptTradeCommand : ProtectedRouteCommand
    {
        private readonly string _tradeId;
        private readonly string _cardId;
        private readonly IUserManager _userManager;

        public AcceptTradeCommand(IUserManager userManager, string tradeId, string cardId)
        {
            _userManager = userManager;
            _tradeId = tradeId;
            _cardId = cardId;
        }

        public override Response Execute()
        {
            Console.WriteLine(_tradeId);
            return _userManager.AcceptTrade(User.Username, _tradeId, _cardId)
                ? new Response {StatusCode = StatusCode.Ok}
                : new Response {StatusCode = StatusCode.Conflict};
        }
    }
}