using System.Collections.Generic;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Cards
{
    /// <summary>
    /// Sets deck of user
    /// </summary>
    public class SetDeckCommand : ProtectedRouteCommand
    {
        private readonly List<string> _cardIds;
        private readonly IUserManager _userManager;

        /// <summary>
        /// Sets user manager and card ids
        /// </summary>
        /// <param name="userManager">the user manager</param>
        /// <param name="cardIds">ids of the cards user wants to be his deck</param>
        public SetDeckCommand(IUserManager userManager, List<string> cardIds)
        {
            _userManager = userManager;
            _cardIds = cardIds;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            var response = new Response();
            var result = false;
            if (_cardIds?.Count == 4) result = _userManager.SetDeck(User.Username, _cardIds);
            response.StatusCode = !result ? StatusCode.BadRequest : StatusCode.Ok;
            return response;
        }
    }
}