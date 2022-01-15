using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Rules;

namespace MonsterTradingCardsGameServer.Cards
{
    /// <summary>
    /// Base Card class
    /// </summary>
    public abstract class Card
    {
        /// <summary>
        /// Sets all basic attributes
        /// </summary>
        /// <param name="id">the uid of the card</param>
        /// <param name="damage">the damage of the card</param>
        /// <param name="mod">the modification of the card</param>
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

        /// <summary>
        /// Converts the card into a universal card
        /// </summary>
        /// <returns>a universal card</returns>
        public abstract UniversalCard ToUniversalCard();

        /// <summary>
        /// Generates the card name
        /// </summary>
        /// <returns>the card name</returns>
        public abstract string GetCardName();

        /// <summary>
        /// Converts the card into a readable card
        /// </summary>
        /// <returns>A ReadableCard</returns>
        public ReadableCard ToReadableCard()
        {
            return new ReadableCard(Id.ToString(), GetCardName(), Damage);
        }

        public abstract string GetCardType();

        /// <summary>
        /// Applies the specific rules for the card
        /// </summary>
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