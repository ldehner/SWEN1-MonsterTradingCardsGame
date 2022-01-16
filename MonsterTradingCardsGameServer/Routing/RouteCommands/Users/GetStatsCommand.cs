using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Users
{
    public class GetStatsCommand : ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;

        public GetStatsCommand(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public override Response Execute()
        {
            Stats stats;
            try
            {
                stats = _userManager.GetUserStats(User.Username);
            }
            catch (UserNotFoundException)
            {
                stats = null;
            }

            var response = new Response();
            if (stats == null)
            {
                response.StatusCode = StatusCode.NotFound;
            }
            else
            {
                response.Payload = JsonConvert.SerializeObject(stats);
                response.StatusCode = StatusCode.Ok;
            }

            return response;
        }
    }
}