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

        public Card(Guid id, int damage, Modification mod)
        {
            Id = id;
            Damage = damage;
            Mod = mod;
            Rules = CardRuleAdder.AddRules(this);
            
        }

        public abstract UniversalCard ToUniversalCard();

        public abstract string GetCardName();

    }
}