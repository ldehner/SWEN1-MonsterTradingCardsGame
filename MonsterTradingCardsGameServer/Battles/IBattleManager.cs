using System.Collections.Generic;

namespace MonsterTradingCardsGameServer.Battles
{
    public interface IBattleManager
    {
        BattleResult NewBattle(string username);

        BattleResult GetBattle(string username, string battleId);

        List<BattleResult> ListBattles(string username);
    }
}