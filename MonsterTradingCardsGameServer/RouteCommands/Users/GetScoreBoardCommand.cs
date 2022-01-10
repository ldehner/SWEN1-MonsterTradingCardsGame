using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.RouteCommands.Users
{
    public class GetScoreBoardCommand : ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;
        public GetScoreBoardCommand(IUserManager userManager)
        {
            _userManager = userManager;
        }
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