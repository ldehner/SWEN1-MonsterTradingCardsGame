using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Manager;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Users
{
    /// <summary>
    /// Gets the score board
    /// </summary>
    public class GetScoreBoardCommand : ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;

        /// <summary>
        /// Sets the user manager
        /// </summary>
        /// <param name="userManager">the user manager</param>
        public GetScoreBoardCommand(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            var scoreBoard = _userManager.GetScores();
            var response = new Response();
            if (scoreBoard == null)
            {
                response.StatusCode = StatusCode.Conflict;
            }
            else
            {
                response.Payload = JsonConvert.SerializeObject(scoreBoard);
                response.StatusCode = StatusCode.Ok;
            }

            return response;
        }
    }
}