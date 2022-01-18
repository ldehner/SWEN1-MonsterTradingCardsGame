using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.DAL
{
    /// <summary>
    ///     Interface for package repository
    /// </summary>
    public interface IPackageRepository
    {
        /// <summary>
        ///     Adds a new package into the db
        /// </summary>
        /// <param name="package">universal card list</param>
        /// <param name="id">uid of the package</param>
        /// <returns>if query was successful</returns>
        public bool AddPackage(List<UniversalCard> package, Guid id);

        /// <summary>
        ///     Adds an quicred package into the stack of the user and updates the number of coins
        /// </summary>
        /// <param name="username">wanted user</param>
        /// <param name="coins">users number of coins</param>
        /// <param name="stack">users stack</param>
        /// <returns>if query was successful</returns>
        public bool AquirePackage(string username, int coins, Stack stack);
    }
}