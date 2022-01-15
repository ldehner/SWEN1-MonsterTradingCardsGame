using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.RouteCommands.Battles
{
    public class StartBattleCommand : ProtectedRouteCommand
    {
        private readonly IBattleManager _battleManager;
        private readonly User _currentUser;

        public StartBattleCommand(IBattleManager battleManager, User currentUser)
        {
            _battleManager = battleManager;
            _currentUser = currentUser;
        }

        public override Response Execute()
        {
            var response = new Response();
            try
            {
                response.Payload = JsonConvert.SerializeObject(_battleManager.NewBattle(_currentUser.Username));
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