namespace MonsterTradingCardsGame.App
{
    public class Spell:Card
    {
        public Spell(int damage, Modification mod):base(damage, mod)
        {
            
        }

        public override string GetCardName()
        {
            return Mod + "-Spell";
        }

        public override void CalculateDamage()
        {
            throw new System.NotImplementedException();
        }
    }
}