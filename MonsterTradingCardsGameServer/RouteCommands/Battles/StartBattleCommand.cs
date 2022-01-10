using System.Text;
using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Core.Authentication;
using MonsterTradingCardsGameServer.Core.Request;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.RouteCommands.Battles
{
    public class StartBattleCommand : ProtectedRouteCommand
    {
        private IBattleManager _battleManager;
        private readonly string _currentUsername;
        private readonly IIdentityProvider _identityProvider;
        
        public StartBattleCommand(IBattleManager battleManager, RequestContext request, IIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
            _currentUsername = ((User)_identityProvider.GetIdentyForRequest(request)).Username;
            _battleManager = battleManager;
        }
        public override Response Execute()
        {

            var response = new Response();
            try
            {
                response.Payload = JsonConvert.SerializeObject(_battleManager.NewBattle(_currentUsername));
                response.StatusCode = StatusCode.Ok;
            }
            catch (BattleFailedException)
            {
                response.StatusCode = StatusCode.Conflict;
            }
            return response;
        }
    }
}