using System.Collections.Generic;
using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Core.Authentication;

namespace MonsterTradingCardsGameServer.Users
{
    /// <summary>
    ///     USer
    /// </summary>
    public class User : ISimpleUser, IIdentity
    {
        public List<BattleResult> Battles;

        /// <summary>
        ///     Sets all attributes
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="stats">users stats</param>
        /// <param name="userData">users data</param>
        /// <param name="stack">users stack</param>
        /// <param name="deck">users deck</param>
        /// <param name="coins">users coins</param>
        public User(string username, Stats stats, UserData userData, Stack stack, Deck deck, int coins)
        {
            Username = username;
            UserData = userData;
            Stats = stats;
            Stack = stack;
            Deck = deck;
            Coins = coins;
            Battles = new List<BattleResult>();
        }

        public sealed override string Username { get; set; }
        public sealed override Stats Stats { get; set; }
        public int Coins { get; set; }
        public string HashedPassword { get; set; }
        public string Token => $"{Username}-mtcgToken";
        public UserData UserData { get; set; }
        public Stack Stack { get; set; }
        public Deck Deck { get; set; }

        /// <summary>
        ///     Converts the user into a simple user
        /// </summary>
        /// <returns>a simple user</returns>
        public SimpleUser ToSimpleUser()
        {
            return new SimpleUser(Username, Stats);
        }
    }
}