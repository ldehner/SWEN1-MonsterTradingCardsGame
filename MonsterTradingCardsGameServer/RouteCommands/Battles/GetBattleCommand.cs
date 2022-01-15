using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.RouteCommands.Battles
{
    public class GetBattleCommand : ProtectedRouteCommand
    {
        private readonly string _battleId;
        private readonly IBattleManager _battleManager;
        private readonly User _currentUser;

        public GetBattleCommand(IBattleManager battleManager, User currentUser, string battleId)
        {
            _battleManager = battleManager;
            _currentUser = currentUser;
            _battleId = battleId;
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                var result = _battleManager.GetBattle(_currentUser.Username, _battleId);
                response.StatusCode = StatusCode.Ok;
                response.Payload = JsonConvert.SerializeObject(result);
            }
            catch (BattleNotFoundException)
            {
                response.StatusCode = StatusCode.NotFound;
            }

            return response;
        }
    }
}