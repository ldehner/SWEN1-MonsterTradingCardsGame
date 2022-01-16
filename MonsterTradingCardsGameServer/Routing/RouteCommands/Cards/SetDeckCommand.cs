using System.Collections.Generic;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Cards
{
    public class SetDeckCommand : ProtectedRouteCommand
    {
        private readonly string _payload;
        private readonly IUserManager _userManager;

        public SetDeckCommand(IUserManager userManager, string payload)
        {
            _userManager = userManager;
            _payload = payload;
        }

        public override Response Execute()
        {
            var response = new Response();
            var result = false;
            var ids = JsonConvert.DeserializeObject<List<string>>(_payload);
            if (ids?.Count == 4) result = _userManager.SetDeck(User.Username, ids);
            response.StatusCode = !result ? StatusCode.BadRequest : StatusCode.Ok;
            return response;
        }
    }
}