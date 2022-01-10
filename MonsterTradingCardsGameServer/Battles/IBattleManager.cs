using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Battles
{
    public interface IBattleManager
    {
        BattleResult NewBattle(string user);
    }
}