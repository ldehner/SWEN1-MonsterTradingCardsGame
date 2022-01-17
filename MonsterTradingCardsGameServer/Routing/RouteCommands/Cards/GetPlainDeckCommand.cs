using System;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Manager;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Cards
{
    public class GetPlainDeckCommand : ProtectedRouteCommand
    {
        private readonly ICardManager _cardManager;
        private readonly string _appendix;

        /// <summary>
        /// Sets user manager
        /// </summary>
        /// <param name="cardManager">card manager</param>
        /// <param name="appendix">the appendix of the request</param>
        public GetPlainDeckCommand(ICardManager cardManager, string appendix)
        {
            _cardManager = cardManager;
            _appendix = appendix;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
        public override Response Execute()
        {
            if (!_appendix.Equals("?format=plain")) return new Response() {StatusCode = StatusCode.BadRequest};
            var result = _cardManager.GetPlainDeck(User.Username);
            return result is null ? new Response(){StatusCode = StatusCode.NoContent} : new Response() {StatusCode = StatusCode.Ok, Payload = result};
        }
    }
}