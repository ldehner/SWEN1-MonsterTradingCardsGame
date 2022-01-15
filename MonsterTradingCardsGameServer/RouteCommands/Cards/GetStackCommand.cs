using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.RouteCommands.Cards
{
    public class GetStackCommand : ProtectedRouteCommand
    {
        private readonly User _currentUser;
        private readonly IUserManager _userManager;

        public GetStackCommand(IUserManager userManager, User currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public override Response Execute()
        {
            var result = _userManager.GetStack(_currentUser.Username);
            var response = new Response();
            if (result is null)
            {
                response.StatusCode = StatusCode.NoContent;
            }
            else
            {
                response.StatusCode = StatusCode.Ok;
                response.Payload = JsonConvert.SerializeObject(result.ToReadableCardList());
            }

            return response;
        }
    }
}