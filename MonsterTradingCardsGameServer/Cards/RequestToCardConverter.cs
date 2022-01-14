using System;

namespace MonsterTradingCardsGameServer.Cards
{
    public static class RequestToCardConverter
    {
        public static UniversalCard ConvertToUniversalCard(string id, int damage, string card)
        {
            var modification = Modification.None;
            var monsterType = MonsterType.None;
            if (card.Contains("Spell"))
            {
                card = card.Replace("Spell", "");
                modification = card switch
                {
                    "Regular" => Modification.Normal,
                    "Fire" => Modification.Fire,
                    "Water" => Modification.Water,
                    _ => modification
                };
            }
            else
            {
                modification = card switch
                {
                    { } a when a.Contains("Fire") => Modification.Fire,
                    { } b when b.Contains("Water") => Modification.Water,
                    _ => Modification.Normal
                };
                card = card.Replace(modification.ToString(), "");
                monsterType = card switch
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
            return new UniversalCard(id, modification, monsterType, damage);
        }
    }
}