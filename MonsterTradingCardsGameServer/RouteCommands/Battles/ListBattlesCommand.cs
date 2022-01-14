using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.RouteCommands.Battles
{
    public class ListBattlesCommand : ProtectedRouteCommand
    {
        private IBattleManager _battleManager;
        private User _currentUser;
        public ListBattlesCommand(IBattleManager battleManager, User currentUser)
        {
            _battleManager = battleManager;
            _currentUser = currentUser;
        }
        public override Response Execute()
        {
            try
            {
                return new Response()
                {
                    StatusCode = StatusCode.Ok,
                    Payload = JsonConvert.SerializeObject(_battleManager.ListBattles(_currentUser.Username))
                };
            }
            catch (UserNotFoundException)
            {
                return new Response()
                {
                    StatusCode = StatusCode.Conflict
                };
            }
        }
    }
}