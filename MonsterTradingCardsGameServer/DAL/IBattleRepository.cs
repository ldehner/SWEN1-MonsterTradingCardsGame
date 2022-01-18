using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.DAL
{
    /// <summary>
    ///     Interface for the battle repositorys
    /// </summary>
    public interface IBattleRepository
    {
        /// <summary>
        ///     Writes a new battle into the db
        /// </summary>
        /// <param name="user">requesting user</param>
        /// <returns>if call was successful</returns>
        bool NewBattle(User user);
    }
}