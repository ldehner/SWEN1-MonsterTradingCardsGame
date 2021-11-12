namespace MonsterTradingCardsGame
{
    public class Monster:Card
    {
        public MonsterType Type { get; }
        
        public Monster(int damage, Modification mod, MonsterType type) : base(damage, mod)
        {
            Type = type;
        }
    }
}