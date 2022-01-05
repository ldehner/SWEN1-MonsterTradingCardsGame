using MonsterTradingCardsGameServer.Core.Authentication;
using MonsterTradingCardsGameServer.Core.Request;
using MonsterTradingCardsGameServer.DAL;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer
{
    public class UserIdentityProvider: IIdentityProvider
    {
        private readonly IUserRepository userRepository;

        public UserIdentityProvider(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IIdentity GetIdentyForRequest(RequestContext request)
        {
            User currentUser = null;

            if (request.Header.TryGetValue("Authorization", out string authToken))
            {
                const string prefix = "Basic ";
                if (authToken.StartsWith(prefix))
                {
                    currentUser = userRepository.GetUserByAuthToken(authToken.Substring(prefix.Length));
                }
            }

            return currentUser;
        }
    }
}