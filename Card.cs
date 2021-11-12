namespace MonsterTradingCardsGame
{
    public abstract class Card
    {
        public readonly int Damage;
        public readonly Modification Mod;

        public Card(int damage, Modification mod)
        {
            Damage = damage;
            Mod = mod;
        }
        
    }
}