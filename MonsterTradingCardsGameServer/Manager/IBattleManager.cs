using System.Collections.Generic;
using MonsterTradingCardsGameServer.Battles;

namespace MonsterTradingCardsGameServer.Manager
{
    /// <summary>
    /// Interface for the battlemanager
    /// </summary>
    public interface IBattleManager
    {
        /// <summary>
        /// Method for creating a new battle
        /// </summary>
        /// <param name="username">username of the requesting user</param>
        /// <returns></returns>
        BattleResult NewBattle(string username);

        /// <summary>
        /// Method for getting an already played battle
        /// </summary>
        /// <param name="username">requesting user</param>
        /// <param name="battleId">id of the battle</param>
        /// <returns></returns>
        BattleResult GetBattle(string username, string battleId);

        /// <summary>
        /// Lists all battles of a user
        /// </summary>
        /// <param name="username">requesting user</param>
        /// <returns></returns>
        List<BattleResult> ListBattles(string username);
    }
}