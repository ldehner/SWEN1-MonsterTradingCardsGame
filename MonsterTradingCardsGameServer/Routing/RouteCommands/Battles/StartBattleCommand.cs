using System;
using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Manager;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Battles
{
    /// <summary>
    /// Starts a new battle
    /// </summary>
    public class StartBattleCommand : ProtectedRouteCommand
    {
        private readonly IBattleManager _battleManager;

        /// <summary>
        /// sets the battle manager
        /// </summary>
        /// <param name="battleManager">the battle manager</param>
        public StartBattleCommand(IBattleManager battleManager)
        {
            _battleManager = battleManager;
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
                response.Payload = JsonConvert.SerializeObject(_battleManager.NewBattle(User.Username));
                response.StatusCode = StatusCode.Ok;
            }
            catch (Exception)
            {
                response.StatusCode = StatusCode.Conflict;
            }

            return response;
        }
    }
}