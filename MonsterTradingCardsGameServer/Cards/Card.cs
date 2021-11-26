using System.Collections.Generic;
using MonsterTradingCardsGameServer.Rules;

namespace MonsterTradingCardsGameServer.Cards
{
    public abstract class Card
    {
        public int Damage { get; set; }
        public readonly Modification Mod;
        public readonly List<Rule> Rules;

        public Card(int damage, Modification mod)
        {
            Damage = damage;
            Mod = mod;
            Rules = new List<Rule>();
        }

        public abstract string GetCardName();

    }
}