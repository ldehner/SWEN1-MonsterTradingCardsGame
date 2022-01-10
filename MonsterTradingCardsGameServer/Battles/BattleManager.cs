using System;
using System.Collections.Generic;
using System.Threading;
using MonsterTradingCardsGameServer.DAL;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Battles
{
    public class BattleManager : IBattleManager
    {
        private Queue<User> _userQ;
        private Dictionary<User, BattleResult> _results;
        private object _battleLock = new();
        private UserManager _userManager;
        private IBattleRepository _battleRepository;
        

        public BattleManager(UserManager userManager, IBattleRepository battleRepository)
        {
            _userQ = new Queue<User>();
            _results = new Dictionary<User, BattleResult>();
            _userManager = userManager;
            _battleRepository = battleRepository;
        }

        public BattleResult NewBattle(string username)
        {
            var user = _userManager.GetUser(username);
            user.Deck = UserGenerator.GenerateUser("username").Deck;
            User u1 = null;
            User u2 = null;
            lock (_battleLock)
            {
                if (!_userQ.Contains(user)) _userQ.Enqueue(user);
                // if already two players inside Queue start a new Battle and delete Players from queue
                // else ignore this step
                if (_userQ.Count >= 2)
                {
                    u1 = _userQ.Dequeue();   // get player 1
                    u2 = _userQ.Dequeue();   // get player 2
                }
            }

            if (u1 != null && u2 != null)
            {
                var result = (new Battle(u1, u2)).StartBattle();   // start battle and get result
                _results.Add(u1, result);   // add player 1 to result dictionary
                _results.Add(u2, result);   // add player 2 to result dictionary
            }

            // wait while battle isn't finished
            while (!_results.ContainsKey(user)) Thread.Sleep(500);
            
            // get result 
            var battleResult = _results[user];

            // remove from result dictionary
            _results.Remove(user);
            
            user.Battles.Add(battleResult);
            if (!_battleRepository.NewBattle(user)) throw new BattleFailedException();
            
            return battleResult;
        }
        
    }
}