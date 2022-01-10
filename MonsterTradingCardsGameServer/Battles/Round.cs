using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.Battles
{
    public class Round
    {
        private Card _c1, _c2;
        private List<string> BattleLog { get; set; }
        public Round(Card c1, Card c2, List<string> battleLog)
        {
            _c1 = c1;
            _c2 = c2;
            BattleLog = battleLog;
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
                    BattleLog.Add(winner.Mod + "-" + ((Monster) winner).Type + " won with " + winner.Damage +
                                    " Damage");
                }
                else
                {
                    BattleLog.Add(winner.Mod + "-Spell won with " + winner.Damage + " Damage");
                }
                if (loser.GetType() == typeof(Monster))
                {
                    BattleLog.Add(loser.Mod + "-" + ((Monster) loser).Type + " lost with " + loser.Damage +
                                     " Damage");
                }
                else
                {
                    BattleLog.Add(loser.Mod + "-Spell lost with " + loser.Damage + " Damage");
                }
            }
            else
            {
                BattleLog.Add("Draw:");
                if (winner.GetType() == typeof(Monster))
                {
                    BattleLog.Add(winner.Mod + "-" + ((Monster) winner).Type + " with " + winner.Damage + " Damage");
                }
                else
                {
                    BattleLog.Add(winner.Mod + "-Spell with " + winner.Damage + " Damage");
                }
                if (loser.GetType() == typeof(Monster))
                {
                    BattleLog.Add(loser.Mod + "-" + ((Monster) loser).Type + " with " + loser.Damage + " Damage");
                }
                else
                {
                    BattleLog.Add(loser.Mod + "-Spell with " + loser.Damage + " Damage");
                }
            }
            
            
        }

        private void _checkRules()
        {
            _c1.Rules.ForEach(rule => rule.CalculateDamage(_c1, _c2));
            _c2.Rules.ForEach(rule => rule.CalculateDamage(_c2, _c1));
            /*foreach (var rule in _c1.Rules)
            {
                rule.CalculateDamage(_c1, _c2); 
            }
            foreach (var rule in _c2.Rules)
            {
                rule.CalculateDamage(_c2, _c1); 
            }*/
        }
        
    }
}