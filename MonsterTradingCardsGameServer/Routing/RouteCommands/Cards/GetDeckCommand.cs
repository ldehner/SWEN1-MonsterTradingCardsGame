using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Cards
{
    /// <summary>
    /// gets users deck
    /// </summary>
    public class GetDeckCommand : ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;

        /// <summary>
        /// Sets user manager
        /// </summary>
        /// <param name="userManager">user manager</param>
        public GetDeckCommand(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
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