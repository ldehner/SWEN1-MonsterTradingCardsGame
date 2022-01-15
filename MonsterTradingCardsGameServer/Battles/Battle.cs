using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Battles
{
    public class Battle
    {
        private const int MaxRounds = 100;
        private Card _current1, _current2;
        private Card _currentBackup1, _currentBackup2;
        private readonly Random _rand = new();
        private int _roundCounter = 1;
        private readonly List<Card> _tmpDeck1;
        private readonly List<Card> _tmpDeck2;
        private readonly User _user1;
        private readonly User _user2;
        public List<string> BattleLog = new();
        public User Winner, Loser;

        public Battle(User user1, User user2)
        {
            _user1 = user1;
            _user2 = user2;
            _tmpDeck1 = new List<Card>(_user1.Deck.Cards);
            _tmpDeck2 = new List<Card>(_user2.Deck.Cards);
        }

        public BattleResult StartBattle()
        {
            while (_roundCounter <= MaxRounds && _tmpDeck1.Count > 0 && _tmpDeck2.Count > 0)
            {
                _pickRandomCard();
                BattleLog.Add("### Round " + _roundCounter + " ###");
                switch (new Round(_current1, _current2, BattleLog).Calculate())
                {
                    case BattleStatus.Draw:
                        break;
                    case BattleStatus.Player1:
                        _tmpDeck1.Add(_currentBackup2);
                        _tmpDeck2.Remove(_currentBackup2);
                        break;
                    case BattleStatus.Player2:
                        _tmpDeck2.Add(_currentBackup1);
                        _tmpDeck1.Remove(_currentBackup1);
                        break;
                }

                _roundCounter++;
            }

            _checkOutcome();
            return new BattleResult(Guid.NewGuid().ToString(), Winner.ToSimpleUser(), Loser.ToSimpleUser(), BattleLog);
        }

        private void _pickRandomCard()
        {
            _current1 = _tmpDeck1[_rand.Next(_tmpDeck1.Count)];
            _current2 = _tmpDeck2[_rand.Next(_tmpDeck2.Count)];
            _currentBackup1 = _current1;
            _currentBackup2 = _current2;
        }

        private void _checkOutcome()
        {
            BattleLog.Add("-------------------");
            BattleLog.Add("### Game Result ###");

            if (_roundCounter > MaxRounds && _tmpDeck1.Count > 0 && _tmpDeck2.Count > 0) BattleLog.Add("DRAW");
            else if (_tmpDeck1.Count > 0 && _tmpDeck2.Count <= 0) _setWinnerLoser(_user1, _user2);
            else if (_tmpDeck1.Count <= 0 && _tmpDeck2.Count > 0) _setWinnerLoser(_user2, _user1);
        }

        private void _setWinnerLoser(User winner, User loser)
        {
            Winner = winner;
            Loser = loser;
            winner.Stats.Wins++;
            winner.Stats.Elo += 3;
            loser.Stats.Losses++;
            if (loser.Stats.Elo < 5) loser.Stats.Elo = 0;
            else loser.Stats.Elo -= 5;
            BattleLog.Add($"{Winner.Username} won");
            BattleLog.Add($"{Loser.Username} lost");
        }
    }
}