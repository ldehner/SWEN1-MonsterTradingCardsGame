using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Core.Response;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Battles
{
    public class StartBattleCommand : ProtectedRouteCommand
    {
        private readonly IBattleManager _battleManager;

        public StartBattleCommand(IBattleManager battleManager)
        {
            _battleManager = battleManager;
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                response.Payload = JsonConvert.SerializeObject(_battleManager.NewBattle(User.Username));
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