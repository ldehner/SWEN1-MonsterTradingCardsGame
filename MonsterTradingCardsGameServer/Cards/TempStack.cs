using System;
using System.Collections.Generic;

namespace MonsterTradingCardsGameServer.Cards
{
    public class TempStack
    {
        public List<StringIdCard> Cards { get; set; }

        public TempStack(List<StringIdCard> cards)
        {
            Cards = cards;
        }

        public void AddCard(StringIdCard card)
        {
            Cards.Add(card);
        }

        public Stack ToStack()
        {
            var cards = new List<Card>();
            Cards.ForEach(card =>
            {
                var monster = new Monster(card.Damage, card.Mod, MonsterType.Elve);
                monster.Id = Guid.Parse(card.Id);
                cards.Add(monster);
                
            });
            return new Stack(cards);
        }
    }
}