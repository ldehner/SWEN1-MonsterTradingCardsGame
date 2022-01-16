using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Cards
{
    public class GetStackCommand : ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;

        public GetStackCommand(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public override Response Execute()
        {
            var result = _userManager.GetStack(User.Username);
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