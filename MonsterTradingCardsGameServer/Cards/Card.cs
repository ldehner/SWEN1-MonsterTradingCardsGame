using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Rules;

namespace MonsterTradingCardsGameServer.Cards
{
    public abstract class Card
    {
        public Card(Guid id, double damage, Modification mod)
        {
            Id = id;
            Damage = damage;
            Mod = mod;
            Rules = new List<Rule>();
        }

        public double Damage { get; set; }
        public Modification Mod { get; set; }
        public List<Rule> Rules { get; set; }
        public Guid Id { get; set; }

        public abstract UniversalCard ToUniversalCard();

        public abstract string GetCardName();

        public ReadableCard ToReadableCard()
        {
            return new ReadableCard(Id.ToString(), GetCardName(), Damage);
        }

        public abstract string GetCardType();

        public virtual void SetRules()
        {
            switch (Mod)
            {
                case Modification.Water:
                    Rules.Add(new WaterRule());
                    break;
                case Modification.Fire:
                    Rules.Add(new FireRule());
                    break;
                case Modification.Normal:
                    Rules.Add(new NormalRule());
                    break;
                case Modification.None:
                default:
                    break;
            }
        }
    }
}