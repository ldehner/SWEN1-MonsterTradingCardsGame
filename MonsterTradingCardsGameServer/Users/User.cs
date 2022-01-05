using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using IIdentity = MonsterTradingCardsGameServer.Core.Authentication.IIdentity;

namespace MonsterTradingCardsGameServer.Users
{
    public class User : IIdentity
    {
        public readonly string Username;
        public string HashedPassword { get; set; }
        public string Token => $"{Username}-msgToken";
        public Stats Stats { get; set; }
        public UserData UserData { get; set; }
        public Stack Stack { get; set; }
        public Deck Deck { get; set; }
        
        public User(string username, Stats stats, UserData userData, Stack stack, Deck deck)
        {
            Username = username;
            UserData = userData;
            Stats = stats;
            Stack = stack;
            Deck = deck;
        }

        public List<Card> AquirePackage()
        {
            if (UserData.Coins > 5)
            {
                var added = new List<Card>(new Package().Cards);
                Stack.Cards.AddRange(added);
                UserData.Coins -= 5;
                return added;
            }

            return new List<Card>();
        }
    }
}