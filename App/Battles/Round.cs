using System;
using MonsterTradingCardsGame.App.Cards;

namespace MonsterTradingCardsGame.App.Battles
{
    public class Round
    {
        private Card _c1, _c2;
        public Round(Card c1, Card c2)
        {
            _c1 = c1;
            _c2 = c2;
        }

        public BattleStatus Calculate()
        {
            _checkRules();
            if (_c1.Damage > _c2.Damage)
            {
                _printRound(_c1, _c2, 1);
                return BattleStatus.Player1;
            }else if (_c1.Damage < _c2.Damage)
            {
                _printRound(_c2, _c1, 1);
                return BattleStatus.Player2;
            }
            _printRound(_c1, _c2, 0);
            return BattleStatus.Draw;
        }

        private void _printRound(Card winner, Card loser, int status)
        {
            if (status == 1)
            {
                if (winner.GetType() == typeof(Monster))
                {
                    Console.WriteLine(winner.Mod+"-"+((Monster) winner).Type+" won with " + winner.Damage + " Damage");
                }
                else
                {
                    Console.WriteLine(winner.Mod+ "-Spell won with " + winner.Damage + " Damage");
                }
                if (loser.GetType() == typeof(Monster))
                {
                    Console.WriteLine(loser.Mod+"-"+((Monster) loser).Type+" lost with " + loser.Damage + " Damage");
                }
                else
                {
                    Console.WriteLine(loser.Mod+ "-Spell lost with " + loser.Damage + " Damage");
                }
            }
            else
            {
                Console.WriteLine("###DRAW###");
                if (winner.GetType() == typeof(Monster))
                {
                    Console.WriteLine(winner.Mod+"-"+((Monster) winner).Type+" with " + winner.Damage + " Damage");
                }
                else
                {
                    Console.WriteLine(winner.Mod+ "-Spell with " + winner.Damage + " Damage");
                }
                if (loser.GetType() == typeof(Monster))
                {
                    Console.WriteLine(loser.Mod+"-"+((Monster) loser).Type+" with " + loser.Damage + " Damage");
                }
                else
                {
                    Console.WriteLine(loser.Mod+ "-Spell with " + loser.Damage + " Damage");
                }
            }
            
            
        }

        private void _checkRules()
        {
            foreach (var rule in _c1.Rules)
            {
                rule.CalculateDamage(_c1, _c2); 
            }
            foreach (var rule in _c2.Rules)
            {
                rule.CalculateDamage(_c2, _c1); 
            }
        }
        
    }
}