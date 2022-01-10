using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Battles
{
    public class Battle
    {
        private const int MaxRounds = 100;
        private int _roundCounter = 1;
        private User _user1, _user2;
        private Card _current1, _current2;
        private Card _currentBackup1, _currentBackup2;
        private List<Card> _tmpDeck1, _tmpDeck2;
        private Random _rand = new();
        private User _winner, _loser;
        private List<string> _battleLog = new();
        
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
                _battleLog.Add("### Round " + _roundCounter + " ###");
                switch ((new Round(_current1, _current2, _battleLog)).Calculate())
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
            return new BattleResult(_winner.ToSimpleUser(), _loser.ToSimpleUser(), _battleLog);
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
            _battleLog.Add("-------------------");
            _battleLog.Add("### Game Result ###");
            if (_roundCounter > MaxRounds && _tmpDeck1.Count > 0 && _tmpDeck2.Count > 0)
            {
                _battleLog.Add("DRAW");
            } else if (_tmpDeck1.Count > 0 && _tmpDeck2.Count <= 0)
            {
                _user1.Stack.Cards.AddRange(_user2.Deck.Cards);
                _removeCards(_user2);
                _user1.Stats.Wins++;
                _user1.Stats.Elo += 3;
                _winner = _user1;
                _loser = _user2;
            }
            else if (_tmpDeck1.Count <= 0 && _tmpDeck2.Count > 0)
            {
                _user2.Stack.Cards.AddRange(_user1.Deck.Cards);
                _removeCards(_user1);
                _user2.Stats.Wins++;
                _user2.Stats.Elo += 3;
                _winner = _user2;
                _loser = _user1;
            }
            _battleLog.Add(_winner.Username);
            _battleLog.Add(" won");
            _battleLog.Add(_loser.Username);
            _battleLog.Add(" lost");
        }

        private static void _removeCards(User user)
        {
            user.Deck.Cards.ToList().ForEach(card => user.Stack.Cards.Remove(card));
            // foreach (var card in user.Deck.Cards.ToList())
            // {
            //     user.Stack.Cards.Remove(card);
            // }
            user.Deck.Cards = new List<Card>();
            user.Stats.Losses++;
            if (user.Stats.Elo < 5)
            {
                user.Stats.Elo = 0;
            }
            else
            {
                user.Stats.Elo -= 5;
            }
        }

        /*private void CheckMods()
        {
            if (_current1.Mod == _current2.Mod)
            {
                    
            }else if (_current1.Mod == Modification.Normal && _current2.Mod == Modification.Water)
            {
                _current1.Damage *= 2;
                _current2.Damage /= 2;
            }else if (_current1.Mod == Modification.Normal && _current2.Mod == Modification.Fire)
            {
                _current1.Damage /= 2;
                _current2.Damage *= 2;
            }
            else if (_current1.Mod == Modification.Water && _current2.Mod == Modification.Normal)
            {
                _current1.Damage /= 2;
                _current2.Damage *= 2;
            }else if (_current1.Mod == Modification.Water && _current2.Mod == Modification.Fire)
            {
                _current1.Damage *= 2;
                _current2.Damage /= 2;
            }else if (_current1.Mod == Modification.Fire && _current2.Mod == Modification.Normal)
            {
                _current1.Damage *= 2;
                _current2.Damage /= 2;
            }else if (_current1.Mod == Modification.Fire && _current2.Mod == Modification.Water)
            {
                _current1.Damage /= 2;
                _current2.Damage *= 2;
            }
        }*/

        /*private void CheckRules()
        {
            if (_current1.GetType().IsInstanceOfType(typeof(Monster)) && _current2.GetType().IsInstanceOfType(typeof(Monster)))
            {
                var tmp1 = (Monster) _current1;
                var tmp2 = (Monster) _current2;
                if (tmp1.Type == MonsterType.Goblin && tmp2.Type == MonsterType.Dragon)
                {
                    _current1.Damage = 0;
                } else if (tmp1.Type == MonsterType.Dragon && tmp2.Type == MonsterType.Goblin)
                {
                    _current2.Damage = 0;
                } else if (tmp1.Type == MonsterType.Wizard && tmp2.Type == MonsterType.Org)
                {
                    _current2.Damage = 0;
                } else if (tmp1.Type == MonsterType.Org && tmp2.Type == MonsterType.Wizard)
                {
                    _current1.Damage = 0;
                } else if (tmp1.Type == MonsterType.Elve && tmp1.Mod == Modification.Fire &&
                           tmp2.Type == MonsterType.Dragon)
                {
                    _current2.Damage = 0;
                } else if (tmp1.Type == MonsterType.Dragon && tmp2.Type == MonsterType.Elve &&
                           tmp2.Mod == Modification.Fire)
                {
                    _current1.Damage = 0;
                }
            }
            else if (_current1.GetType().IsInstanceOfType(typeof(Monster)) && _current2.GetType().IsInstanceOfType(typeof(Spell)))
            {
                var tmp1 = (Monster) _current1;
                if (tmp1.Type == MonsterType.Kraken)
                {
                    _current2.Damage = 0;
                } else if (tmp1.Type == MonsterType.Knight && _current1.Mod == Modification.Water)
                {
                    _current1.Damage = 0;
                }
            }else if (_current1.GetType().IsInstanceOfType(typeof(Spell)) && _current2.GetType().IsInstanceOfType(typeof(Monster)))
            {
                var tmp2 = (Monster) _current2;
                if (tmp2.Type == MonsterType.Kraken)
                {
                    _current1.Damage = 0;
                } else if (_current1.Mod == Modification.Water && tmp2.Type == MonsterType.Knight)
                {
                    _current2.Damage = 0;
                }
            }
        }*/

        /*private void Round()
        {
            _current1 = _tmpDeck1[_rand.Next(_tmpDeck1.Count)];
            _current2 = _tmpDeck2[_rand.Next(_tmpDeck2.Count)];
            _currentBackup1 = _current1;
            _currentBackup2 = _current2;
                
            CheckRules();
            if (_current1.Damage > 0 && _current2.Damage > 0)
            {
                CheckMods();
            }
            Console.WriteLine("Damage Card 1 ( "+_current1.GetCardName()+") : " + _current1.Damage);
            Console.WriteLine("Damage Card 2 ( "+_current2.GetCardName()+") : " + _current2.Damage);
            if (_current1.Damage > _current2.Damage)
            {
                Console.WriteLine("Card 1 won");
                _tmpDeck1.Add(_currentBackup2);
                _tmpDeck2.Remove(_currentBackup2);
            } else if (_current1.Damage < _current2.Damage)
            {
                Console.WriteLine("Card 2 won");
                _tmpDeck1.Remove(_currentBackup1);
                _tmpDeck2.Add(_currentBackup1);
            }
            _roundCounter++;
        }*/
    }
}