namespace MonsterTradingCardsGameServer.Cards
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
    }
}