namespace MonsterTradingCardsGameServer.Cards
{
    /// <summary>
    /// Card generated from the create package command
    /// </summary>
    public class UserRequestCard
    {
        /// <summary>
        /// sets all attributes
        /// </summary>
        /// <param name="id">uid of the card</param>
        /// <param name="name">name of the card</param>
        /// <param name="damage">damage of the card</param>
        public UserRequestCard(string id, string name, double damage)
        {
            Id = id;
            Name = name;
            Damage = damage;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public double Damage { get; set; }

        /// <summary>
        /// Converts the request card into a universal card
        /// </summary>
        /// <returns>the universal card</returns>
        public UniversalCard ToUniversalCard()
        {
            Modification modification;
            var monsterType = MonsterType.None;
            if (Name.Contains("Spell"))
            {
                Name = Name.Replace("Spell", "");
                modification = Name switch
                {
                    "Regular" => Modification.Normal,
                    "Fire" => Modification.Fire,
                    "Water" => Modification.Water,
                    _ => throw new CardNotFoundException()
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
                    "Elf" => MonsterType.Elf,
                    _ => throw new CardNotFoundException()
                };
            }

            return new UniversalCard(Id, modification, monsterType, Damage);
        }
    }
}