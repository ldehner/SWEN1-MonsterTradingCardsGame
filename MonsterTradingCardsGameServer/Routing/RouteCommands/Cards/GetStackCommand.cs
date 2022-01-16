using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Cards
{
    /// <summary>
    /// Gets users stack
    /// </summary>
    public class GetStackCommand : ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;

        /// <summary>
        /// sets user manager
        /// </summary>
        /// <param name="userManager">user manager</param>
        public GetStackCommand(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
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