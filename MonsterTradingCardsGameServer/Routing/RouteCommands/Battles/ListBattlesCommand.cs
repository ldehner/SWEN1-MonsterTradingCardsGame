using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Battles
{
    public class ListBattlesCommand : ProtectedRouteCommand
    {
        private readonly IBattleManager _battleManager;

        public ListBattlesCommand(IBattleManager battleManager)
        {
            _battleManager = battleManager;
        }

        public override Response Execute()
        {
            try
            {
                return new Response
                {
                    StatusCode = StatusCode.Ok,
                    Payload = JsonConvert.SerializeObject(_battleManager.ListBattles(User.Username))
                };
            }
            catch (UserNotFoundException)
            {
                return new Response
                {
                    StatusCode = StatusCode.Conflict
                };
            }
        }
    }
}