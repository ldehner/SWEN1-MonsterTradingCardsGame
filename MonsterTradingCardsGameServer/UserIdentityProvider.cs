using MonsterTradingCardsGameServer.Core.Authentication;
using MonsterTradingCardsGameServer.Core.Request;
using MonsterTradingCardsGameServer.DAL;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer
{
    public class UserIdentityProvider : IIdentityProvider
    {
        private readonly IUserRepository _userRepository;

        public UserIdentityProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IIdentity GetIdentityForRequest(RequestContext request)
        {
            User currentUser = null;

            if (!request.Header.TryGetValue("Authorization", out var authToken)) return null;
            const string prefix = "Basic ";
            if (authToken.StartsWith(prefix))
                currentUser = _userRepository.GetUserByAuthToken(authToken[prefix.Length..]);

            return currentUser;
        }
    }
}