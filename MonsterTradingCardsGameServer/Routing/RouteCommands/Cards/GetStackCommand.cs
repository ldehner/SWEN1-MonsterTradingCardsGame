using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Manager;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Cards
{
    /// <summary>
    /// Gets users stack
    /// </summary>
    public class GetStackCommand : ProtectedRouteCommand
    {
        private readonly ICardManager _cardManager;

        /// <summary>
        /// sets user manager
        /// </summary>
        /// <param name="cardManager">card manager</param>
        public GetStackCommand(ICardManager cardManager)
        {
            _cardManager = cardManager;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            var result = _cardManager.GetStack(User.Username);
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