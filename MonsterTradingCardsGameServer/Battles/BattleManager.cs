using System.Collections.Generic;
using System.Threading;
using MonsterTradingCardsGameServer.DAL;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Battles
{
    /// <summary>
    /// Handles battles of all users
    /// </summary>
    public class BattleManager : IBattleManager
    {
        private readonly object _battleLock = new();
        private readonly IBattleRepository _battleRepository;
        private readonly Dictionary<User, BattleResult> _results;
        private readonly UserManager _userManager;
        private readonly Queue<User> _userQ;

        /// <summary>
        /// Sets all attributes
        /// </summary>
        /// <param name="userManager">the user manager</param>
        /// <param name="battleRepository">the battle repository</param>
        public BattleManager(UserManager userManager, IBattleRepository battleRepository)
        {
            _userQ = new Queue<User>();
            _results = new Dictionary<User, BattleResult>();
            _userManager = userManager;
            _battleRepository = battleRepository;
        }

        /// <summary>
        /// Creates a new battle
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>the battle result</returns>
        /// <exception cref="InvalidDeckException">exception in case deck is invalid</exception>
        /// <exception cref="BattleFailedException">exception in case battle failed</exception>
        public BattleResult NewBattle(string username)
        {
            var user = _userManager.GetUser(username);
            if (user.Deck.Cards.Count != 4) throw new InvalidDeckException();
            User u1 = null;
            User u2 = null;
            lock (_battleLock)
            {
                //if (!_userQ.Contains(user)) _userQ.Enqueue(user); // does not work
                if (_userQ.Count > 0 && _userQ.Peek().Username.Equals(user.Username)) throw new AlreadyInQueueException();
                _userQ.Enqueue(user);
                // if already two players inside Queue start a new Battle and delete Players from queue
                // else ignore this step
                if (_userQ.Count >= 2)
                {
                    u1 = _userQ.Dequeue(); // get player 1
                    u2 = _userQ.Dequeue(); // get player 2
                }
            }

            if (u1 != null && u2 != null)
            {
                var result = new Battle(u1, u2).StartBattle(); // start battle and get result
                _results.Add(u1, result); // add player 1 to result dictionary
                _results.Add(u2, result); // add player 2 to result dictionary
            }

            // wait while battle isn't finished
            while (!_results.ContainsKey(user)) Thread.Sleep(500);

            // get result 
            var battleResult = _results[user];

            // remove from result dictionary
            _results.Remove(user);

            user.Battles.Add(battleResult);
            if (!_battleRepository.NewBattle(user)) throw new BattleFailedException();

            _battleRepository.NewBattle(user);
            return battleResult;
        }

        /// <summary>
        /// Gets a battle
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="battleId">specific battle id</param>
        /// <returns>returns the battle result of the battle</returns>
        /// <exception cref="BattleNotFoundException">throws exception if user was not found</exception>
        public BattleResult GetBattle(string username, string battleId)
        {
            var user = _userManager.GetUser(username);
            BattleResult battleResult = null;
            user.Battles.ForEach(battle =>
            {
                if (battle.Guid.ToString().Equals(battleId)) battleResult = battle;
            });
            if (battleResult is null) throw new BattleNotFoundException();
            return battleResult;
        }

        /// <summary>
        /// Lists all battles of user
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>the list of battle results</returns>
        public List<BattleResult> ListBattles(string username)
        {
            return _userManager.GetUser(username).Battles;
        }
    }
}