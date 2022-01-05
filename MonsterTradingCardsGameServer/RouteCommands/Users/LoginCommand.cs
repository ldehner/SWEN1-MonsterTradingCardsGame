using MonsterTradingCardsGameServer.Core.Authentication;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Core.Routing;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.RouteCommands.Users
{
    public class LoginCommand : IRouteCommand
    {
        private readonly IUserManager userManager;

        public Credentials Credentials { get; private set; }

        public LoginCommand(IUserManager userManager, Credentials credentials)
        {
            Credentials = credentials;
            this.userManager = userManager;
        }
        public Response Execute()
        {
            User user;
            try
            {
                user = userManager.LoginUser(Credentials);
            }
            catch (UserNotFoundException)
            {
                user = null;
            }

            var response = new Response();
            if (user == null)
            {
                response.StatusCode = StatusCode.Unauthorized;
            }
            else
            {
                response.StatusCode = StatusCode.Ok;
                response.Payload = user.Token;
            }

            return response;
        }
    }
}