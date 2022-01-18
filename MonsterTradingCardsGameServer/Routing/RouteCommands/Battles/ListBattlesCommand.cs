using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Manager;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Battles
{
    /// <summary>
    ///     Lists all battles of a user
    /// </summary>
    public class ListBattlesCommand : ProtectedRouteCommand
    {
        private readonly IBattleManager _battleManager;

        /// <summary>
        ///     Sets the battle manager
        /// </summary>
        /// <param name="battleManager">the battle manager</param>
        public ListBattlesCommand(IBattleManager battleManager)
        {
            _battleManager = battleManager;
        }

        /// <summary>
        ///     Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
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