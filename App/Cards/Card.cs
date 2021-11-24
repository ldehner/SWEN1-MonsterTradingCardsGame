using System;

namespace MonsterTradingCardsGame.App
{
    public abstract class Card
    {
        public int Damage { get; set; }
        public readonly Modification Mod;

        public Card(int damage, Modification mod)
        {
            Damage = damage;
            Mod = mod;
        }

        public abstract string GetCardName();

        public abstract void CalculateDamage();
    }
}