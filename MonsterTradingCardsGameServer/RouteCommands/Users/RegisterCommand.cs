using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Core.Routing;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.RouteCommands.Users
{
    public class RegisterCommand : IRouteCommand
    {
        private readonly IUserManager _userManager;

        public RegisterCommand(IUserManager userManager, Credentials credentials)
        {
            Credentials = credentials;
            _userManager = userManager;
        }

        public Credentials Credentials { get; }

        public Response Execute()
        {
            var response = new Response();
            try
            {
                _userManager.RegisterUser(Credentials);
                response.StatusCode = StatusCode.Created;
            }
            catch (DuplicateUserException)
            {
                response.StatusCode = StatusCode.Conflict;
            }

            return response;
        }
    }
}