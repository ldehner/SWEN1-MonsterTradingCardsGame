using System.Collections.Generic;
using MonsterTradingCardsGame.App.Users;

namespace MonsterTradingCardsGame.App.Battles
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