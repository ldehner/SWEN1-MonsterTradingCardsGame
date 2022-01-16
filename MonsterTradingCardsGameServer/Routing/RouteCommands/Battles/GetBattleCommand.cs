using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Core.Response;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Battles
{
    /// <summary>
    /// Gets the users desired battle
    /// </summary>
    public class GetBattleCommand : ProtectedRouteCommand
    {
        private readonly string _battleId;
        private readonly IBattleManager _battleManager;

        /// <summary>
        /// sets battle manager and battle id
        /// </summary>
        /// <param name="battleManager">the battle manager</param>
        /// <param name="battleId">the id of the battle, the user desires</param>
        public GetBattleCommand(IBattleManager battleManager, string battleId)
        {
            _battleManager = battleManager;
            _battleId = battleId;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
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