using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.RouteCommands.Cards
{
    public class GetDeckCommand: ProtectedRouteCommand
    {
        private IUserManager _userManager;
        private User _currentUser;
        public GetDeckCommand(IUserManager userManager, User currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }
        public override Response Execute()
        {
            var result = _userManager.GetDeck(_currentUser.Username);
            var response = new Response();
            if (result is null) response.StatusCode = StatusCode.NoContent;
            if(result is not null)
            {
                response.StatusCode = StatusCode.Ok;
                response.Payload = JsonConvert.SerializeObject(result);
            }
            return response;
        }
    }
}