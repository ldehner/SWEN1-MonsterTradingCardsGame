using System.Collections.Generic;
using MonsterTradingCardsGame.App.Cards;
using MonsterTradingCardsGame.App.Users;

namespace MonsterTradingCardsGame.App
{
    public class DatabaseConnector
    {
        public static User ValidateUser(string username, string password)
        {
            var stats = new int[] {1, 2};
            var cards = new List<Card>();
            return new User("bla",40,stats,"fsdfdfs", new Stack(cards), new Deck(cards));
        }

        public static User RegisterUser(string username, string password, string bio)
        {
            var stats = new int[] {1, 2};
            var cards = new List<Card>();
            return new User("bla",40,stats,"fsdfdfs", new Stack(cards), new Deck(cards));
        }
    }
}