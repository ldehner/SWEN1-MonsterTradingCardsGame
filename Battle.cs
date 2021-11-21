using System;
using System.Collections.Generic;
using System.Transactions;

namespace MonsterTradingCardsGame
{
    public class Battle
    {
        private const int MaxRounds = 100;
        private int _roundCounter = 1;
        private User _user1, _user2;
        private Card _current1, _current2;
        private Card _currentBackup1, _currentBackup2;
        private List<Card> _tmpDeck1, _tmpDeck2;
        private Random _rand = new Random();
        
        public Battle(User user1, User user2)
        {
            _user1 = user1;
            _user2 = user2;
            _tmpDeck1 = _user1.Deck.Cards;
            _tmpDeck2 = _user2.Deck.Cards;
            StartBattle();
        }

        public void StartBattle()
        {
            while (_roundCounter <= MaxRounds && _tmpDeck1.Count > 0 && _tmpDeck2.Count > 0)
            {
                Round();
            }
            
            Console.WriteLine(_roundCounter);
            Console.WriteLine(_tmpDeck1.Count);
            Console.WriteLine(_tmpDeck2.Count);

            if (_roundCounter > MaxRounds && _tmpDeck1.Count > 0 && _tmpDeck2.Count > 0)
            {
                Console.WriteLine("Draw");
            } else if (_tmpDeck1.Count > 0 && _tmpDeck2.Count <= 0)
            {
                Console.WriteLine("Player 1 won");
            }
            else if (_tmpDeck1.Count <= 0 && _tmpDeck2.Count > 0)
            {
                Console.WriteLine("Player 2 won");
            }

            //_user1.Deck = null;
        }

        private void CheckMods()
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
        }

        private void CheckRules()
        {
            var monster = new Monster(0, Modification.Normal, MonsterType.Org);
            var spell = new Spell(0, Modification.Normal);
            if (_current1.GetType().IsInstanceOfType(monster) && _current2.GetType().IsInstanceOfType(monster))
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
            else if (_current1.GetType().IsInstanceOfType(monster) && _current2.GetType().IsInstanceOfType(spell))
            {
                var tmp1 = (Monster) _current1;
                if (tmp1.Type == MonsterType.Kraken)
                {
                    _current2.Damage = 0;
                } else if (tmp1.Type == MonsterType.Knight && _current1.Mod == Modification.Water)
                {
                    _current1.Damage = 0;
                }
            }else if (_current1.GetType().IsInstanceOfType(spell) && _current2.GetType().IsInstanceOfType(monster))
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
        }

        private void Round()
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
            Console.WriteLine("Damage Card 1: " + _current1.Damage);
            Console.WriteLine("Damage Card 2: " + _current2.Damage);
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
        }
    }
}