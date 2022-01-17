using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Manager
{
    /// <summary>
    /// Package manager interface
    /// </summary>
    public interface IPackageManager
    {
        /// <summary>
        /// adds an package
        /// </summary>
        /// <param name="package">list of cards</param>
        /// <returns>if query was successful</returns>
        public bool AddPackage(List<UserRequestCard> package);

        /// <summary>
        /// aquires an package
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>if query was successful</returns>
        public bool AquirePackage(User user);
    }
}