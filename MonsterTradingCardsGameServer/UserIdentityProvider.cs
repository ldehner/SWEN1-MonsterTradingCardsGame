using MonsterTradingCardsGameServer.Core.Authentication;
using MonsterTradingCardsGameServer.Core.Request;
using MonsterTradingCardsGameServer.DAL;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer
{
    /// <summary>
    /// Providers identity for user
    /// </summary>
    public class UserIdentityProvider : IIdentityProvider
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Sets all attributes
        /// </summary>
        /// <param name="userRepository">the user repository</param>
        public UserIdentityProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Checks the identity of the user
        /// </summary>
        /// <param name="request">the request context</param>
        /// <returns>the identity of the user</returns>
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