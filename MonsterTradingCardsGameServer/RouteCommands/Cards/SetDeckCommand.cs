using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.RouteCommands.Cards
{
    public class SetDeckCommand: ProtectedRouteCommand
    {
        private IUserManager _userManager;
        private User _currentUser;
        private string _payload;
        public SetDeckCommand(IUserManager userManager, User currentUser, string payload)
        {
            _userManager = userManager;
            _currentUser = currentUser;
            _payload = payload;
        }
        public override Response Execute()
        {
            var response = new Response();
            bool result = false;
            var ids = JsonConvert.DeserializeObject<List<string>>(_payload);
            if (ids?.Count == 4) result = _userManager.SetDeck(_currentUser.Username, ids);
            response.StatusCode = !result ? StatusCode.BadRequest : StatusCode.Ok;
            return response;
            
        }
    }
}