using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;

namespace MonsterTradingCardsGameServer.Battles
{
    /// <summary>
    /// </summary>
    public class Round
    {
        private readonly Card _c1;
        private readonly Card _c2;

        public Round(Card c1, Card c2, List<string> battleLog)
        {
            _c1 = c1;
            _c2 = c2;
            BattleLog = battleLog;
        }

        private List<string> BattleLog { get; }

        /// <summary>
        ///     Calculates which user has won the round
        /// </summary>
        /// <returns>the result of the battle</returns>
        public BattleStatus Calculate()
        {
            _checkRules();
            if (_c1.Damage > _c2.Damage)
            {
                _printRound(_c1, _c2, 1);
                return BattleStatus.Player1;
            }

            if (_c1.Damage < _c2.Damage)
            {
                _printRound(_c2, _c1, 1);
                return BattleStatus.Player2;
            }

            _printRound(_c1, _c2, 0);
            return BattleStatus.Draw;
        }

        /// <summary>
        ///     Adds the result of the current round to the battle log
        /// </summary>
        /// <param name="winner">the winning card</param>
        /// <param name="loser">the losing card</param>
        /// <param name="status">the status of the battle</param>
        private void _printRound(Card winner, Card loser, int status)
        {
            if (status == 1)
            {
                BattleLog.Add($"{winner.GetCardName()} won with {winner.Damage} Damage");
                BattleLog.Add($"{loser.GetCardName()} lost with {loser.Damage} Damage");
            }
            else
            {
                BattleLog.Add("Draw:");
                BattleLog.Add($"{winner.GetCardName()} with {winner.Damage} Damage");
                BattleLog.Add($"{loser.GetCardName()} with {loser.Damage} Damage");
            }
        }

        /// <summary>
        ///     applies the rules and sets the recalculated damage
        /// </summary>
        private void _checkRules()
        {
            _c1.Rules.ForEach(rule => rule.CalculateDamage(_c1, _c2));
            _c2.Rules.ForEach(rule => rule.CalculateDamage(_c2, _c1));
        }
    }
}