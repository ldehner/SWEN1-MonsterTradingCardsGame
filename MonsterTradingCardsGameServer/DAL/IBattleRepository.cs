using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.DAL
{
    public interface IBattleRepository
    {
        bool NewBattle(User user);
        
    }
}