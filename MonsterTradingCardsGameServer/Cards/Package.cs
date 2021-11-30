using System.Collections.Generic;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Cards
{
    public class Package
    {
        public List<Card> Cards { get; private set; }

        public Package()
        {
            Cards = new List<Card>();
        }

        public void AddCardsToUser(User user)
        {
            if (user.Coins >= 5)
            {
                user.Stack.Cards.AddRange(Cards);
                user.Coins -= 5;
            }
            else
            {
                throw new TooFewCoinsException();
            }
        }
    }
}