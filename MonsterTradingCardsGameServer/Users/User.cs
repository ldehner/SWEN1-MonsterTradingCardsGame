using System.Collections.Generic;
using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Cards;
using IIdentity = MonsterTradingCardsGameServer.Core.Authentication.IIdentity;

namespace MonsterTradingCardsGameServer.Users
{
    public class User : ISimpleUser,IIdentity
    {
        //public readonly string Username;
        public sealed override string Username { get; set; }
        public sealed override Stats Stats { get; set; }
        public int Coins { get; set; }
        public string HashedPassword { get; set; }
        public string Token => $"{Username}-mtcgToken";
        public List<BattleResult> Battles;
        public UserData UserData { get; set; }
        public Stack Stack { get; set; }
       public Deck Deck { get; set; }
        
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

        public SimpleUser ToSimpleUser()
        {
            return new SimpleUser(Username, Stats);
        }
    }
}