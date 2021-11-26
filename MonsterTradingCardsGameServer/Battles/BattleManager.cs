using System.Collections.Generic;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Battles
{
    public class BattleManager
    {
        private Queue<User> Userq;
        public BattleManager()
        {
            Userq = new Queue<User>();
        }

        public void NewBattle(User user)
        {
            Userq.Enqueue(user);
            if(Userq.Count >= 2) StartBattle();
        }

        public void StartBattle()
        {
            new Battle(Userq.Dequeue(), Userq.Dequeue());
        }
    }
}