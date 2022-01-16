using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Core.Response;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Battles
{
    public class GetBattleCommand : ProtectedRouteCommand
    {
        private readonly string _battleId;
        private readonly IBattleManager _battleManager;

        public GetBattleCommand(IBattleManager battleManager, string battleId)
        {
            _battleManager = battleManager;
            _battleId = battleId;
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                var result = _battleManager.GetBattle(User.Username, _battleId);
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