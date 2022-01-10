using System.Text;
using MonsterTradingCardsGameServer.Core.Authentication;
using MonsterTradingCardsGameServer.Core.Request;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.RouteCommands.Users
{
    public class GetStatsCommand : ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;
        private readonly User _currentUser;
        
        public GetStatsCommand(IUserManager userManager, User currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }
        public override Response Execute()
        {
            Stats stats;
            try
            {
                stats = _userManager.GetUserStats(_currentUser.Username);
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