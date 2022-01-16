using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Cards
{
    public class GetDeckCommand : ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;

        public GetDeckCommand(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public override Response Execute()
        {
            var result = _userManager.GetDeck(User.Username);
            var response = new Response();
            if (result is null) response.StatusCode = StatusCode.NoContent;
            if (result is null) return response;
            response.StatusCode = StatusCode.Ok;
            response.Payload = JsonConvert.SerializeObject(result.ToReadableCardList());

            return response;
        }
    }
}