using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Rules;

namespace MonsterTradingCardsGameServer.Cards
{
    public class StringIdCard
    {
        public int Damage { get; set; }
        public Modification Mod { get; set; }
        public List<Rule> Rules { get; set; }
        public string Id { get; set; }

        public StringIdCard(int damage, Modification mod)
        {
            Damage = damage;
            Mod = mod;
            Rules = new List<Rule>();
            Id = "Guid.NewGuid()";
        }
    }
}