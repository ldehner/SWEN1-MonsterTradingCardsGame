using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.DAL;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Manager
{
    /// <summary>
    ///     manages packages
    /// </summary>
    public class PackageManager : IPackageManager
    {
        private readonly IPackageRepository _packageRepository;

        /// <summary>
        ///     Sets package repository
        /// </summary>
        /// <param name="packageRepository">the package repository</param>
        public PackageManager(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        /// <summary>
        ///     adds an package
        /// </summary>
        /// <param name="package">list of cards</param>
        /// <returns>if query was successful</returns>
        public bool AddPackage(List<UserRequestCard> package)
        {
            if (package.Count != 5) return false;
            var tmp = new List<UniversalCard>();
            package.ForEach(card => tmp.Add(card.ToUniversalCard()));
            return _packageRepository.AddPackage(tmp, Guid.NewGuid());
        }

        /// <summary>
        ///     aquires an package
        /// </summary>
        /// <param name="user">users username</param>
        /// <returns>if query was successful</returns>
        public bool AquirePackage(User user)
        {
            return user.Coins < 5
                ? throw new TooFewCoinsException()
                : _packageRepository.AquirePackage(user.Username, user.Coins, user.Stack);
        }
    }
}