namespace MonsterTradingCardsGame.App.Cards
{
    public class Monster:Card
    {
        public MonsterType Type { get; }
        
        public Monster(int damage, Modification mod, MonsterType type) : base(damage, mod)
        {
            Type = type;
        }

        public override string GetCardName()
        {
            return Mod + "-" + Type;
        }
    }
}