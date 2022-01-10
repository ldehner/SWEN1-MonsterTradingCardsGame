using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Rules;

namespace MonsterTradingCardsGameServer.Cards
{
    public abstract class Card
    {
        public int Damage { get; set; }
        public Modification Mod { get; set; }
        public List<Rule> Rules { get; set; }
        public Guid Id { get; set; }

        public Card(int damage, Modification mod)
        {
            Damage = damage;
            Mod = mod;
            Rules = new List<Rule>();
            Id = Guid.NewGuid();
        }

        public abstract string GetCardName();

    }
}