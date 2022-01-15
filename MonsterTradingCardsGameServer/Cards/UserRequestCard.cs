namespace MonsterTradingCardsGameServer.Cards
{
    public class UserRequestCard
    {
        public UserRequestCard(string id, string name, double damage)
        {
            Id = id;
            Name = name;
            Damage = damage;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public double Damage { get; set; }

        public UniversalCard ToUniversalCard()
        {
            var modification = Modification.None;
            var monsterType = MonsterType.None;
            if (Name.Contains("Spell"))
            {
                Name = Name.Replace("Spell", "");
                modification = Name switch
                {
                    "Regular" => Modification.Normal,
                    "Fire" => Modification.Fire,
                    "Water" => Modification.Water,
                    _ => modification
                };
            }
            else
            {
                modification = Name switch
                {
                    { } a when a.Contains("Fire") => Modification.Fire,
                    { } b when b.Contains("Water") => Modification.Water,
                    _ => Modification.Normal
                };
                Name = Name.Replace(modification.ToString(), "");
                monsterType = Name switch
                {
                    "Goblin" => MonsterType.Goblin,
                    "Dragon" => MonsterType.Dragon,
                    "Wizard" => MonsterType.Wizard,
                    "Org" => MonsterType.Org,
                    "Knight" => MonsterType.Knight,
                    "Kraken" => MonsterType.Kraken,
                    "Elve" => MonsterType.Elve,
                    _ => monsterType
                };
            }

            return new UniversalCard(Id, modification, monsterType, Damage);
        }
    }
}