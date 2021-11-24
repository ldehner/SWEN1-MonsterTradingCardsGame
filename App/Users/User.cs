using System.Collections.Generic;
using MonsterTradingCardsGame.App.Cards;

namespace MonsterTradingCardsGame.App.Users
{
    public class User
    {
        public readonly string Username;
        
        public int Coins { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public string Bio { get; set; }
        public Stack Stack { get; set; }
        public Deck Deck { get; set; }
        
        public User(string username, int coins, int[] stats, string bio, Stack stack, Deck deck)
        {
            Username = username;
            Coins = coins;
            Wins = stats[0];
            Losses = stats[1];
            Bio = bio;
            Stack = stack;
            Deck = deck;
        }

        public List<Card> AquirePackage()
        {
            if (Coins > 5)
            {
                List<Card> added = new List<Card>(new Package().Cards);
                Stack.Cards.AddRange(added);
                Coins -= 5;
                return added;
            }

            return new List<Card>();
        }
    }
}