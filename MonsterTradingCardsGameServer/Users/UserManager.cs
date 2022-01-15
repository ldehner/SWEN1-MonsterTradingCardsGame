using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.DAL;

namespace MonsterTradingCardsGameServer.Users
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            ActiveUsers = new Dictionary<string, User>();
        }

        private Dictionary<string, User> ActiveUsers { get; set; }

        public User LoginUser(Credentials credentials)
        {
            return _userRepository.GetUserByCredentials(credentials.Username, credentials.Password) ??
                   throw new UserNotFoundException();
        }

        public User GetUser(string username)
        {
            return _userRepository.GetUserByUsername(username) ?? throw new UserNotFoundException();
        }

        public UserData GetUserData(string username)
        {
            return _userRepository.GetUserByUsername(username).UserData ?? throw new UserNotFoundException();
        }


        public void EditUserData(string username, UserData userData)
        {
            if (!_userRepository.UpdateUserData(username, userData)) throw new UserNotFoundException();
        }

        public List<Score> GetScores()
        {
            return _userRepository.GetScoreBoard();
        }

        public Stack GetStack(string username)
        {
            return _userRepository.GetStack(username);
        }

        public Deck GetDeck(string username)
        {
            return _userRepository.GetDeck(username);
        }

        public bool SetDeck(string username, List<string> ids)
        {
            var stack = GetStack(username);
            var newDeck = new List<Card>();
            ids.ForEach(cardId => stack.Cards.ForEach(card =>
            {
                var result = card.Id == Guid.Parse(cardId);
                if (result) newDeck.Add(card);
            }));
            return newDeck.Count == 4 && _userRepository.SetDeck(username, new Deck(newDeck));
        }

        public bool AddPackage(string username, List<UserRequestCard> package)
        {
            if (package.Count != 5) return false;
            var tmp = new List<UniversalCard>();
            package.ForEach(card => tmp.Add(card.ToUniversalCard()));
            return _userRepository.AddPackage(username, tmp, Guid.NewGuid());
        }

        public Stats GetUserStats(string username)
        {
            return _userRepository.GetUserByUsername(username).Stats ?? throw new UserNotFoundException();
        }

        public void RegisterUser(Credentials credentials)
        {
            var user = new User(credentials.Username, new Stats(0, 0),
                new UserData(credentials.Username, "Hey, ich bin neu!", ":)"),
                new Stack(new List<Card>()), new Deck(new List<Card>()), 20);
            if (!_userRepository.InsertUser(user, credentials.Password)) throw new DuplicateUserException();
        }

        public bool AquirePackage(string username)
        {
            var user = GetUser(username);
            return user.Coins >= 5 && _userRepository.AquirePackage(username, user.Coins, user.Stack);
        }

        public bool CreateTrade(string username, TradingDeal tradingDeal)
        {
            var user = GetUser(username);
            var cardInDeck = false;
            Card tradingCard = null;
            user.Stack.Cards.ForEach(card =>
            {
                if (card.Id.ToString().Equals(tradingDeal.CardToTrade)) tradingCard = card;
            });
            user.Deck.Cards.ForEach(card =>
            {
                if (card.Id.ToString().Equals(tradingDeal.CardToTrade)) cardInDeck = true;
            });
            user.Stack.Cards.Remove(tradingCard);
            return !cardInDeck && tradingCard is not null &&
                   _userRepository.CreateTrade(username, tradingCard, tradingDeal.MinimumDamage, tradingDeal.Id,
                       tradingDeal.Type.Equals("Monster") ? 1 : 0) && _userRepository.SetStack(user);
        }

        public List<TradingOffer> ListTrades()
        {
            return _userRepository.ListTrades();
        }

        public bool AcceptTrade(string username, string tradeId, string cardId)
        {
            var cardInDeck = false;
            Card tradingCard = null;
            var buyer = _userRepository.GetUserByUsername(username);
            buyer.Stack.Cards.ForEach(card =>
            {
                if (card.Id.ToString().Equals(cardId)) tradingCard = card;
            });
            buyer.Deck.Cards.ForEach(card =>
            {
                if (card.Id.ToString().Equals(cardId)) cardInDeck = true;
            });
            if (tradingCard is null || cardInDeck) return false;
            var deal = _userRepository.GetTrade(tradeId);
            if (deal.Trader.Equals(username) || tradingCard.Damage < deal.RequiredDamage ||
                !tradingCard.GetCardType().Equals(deal.RequiredType)) return false;
            var seller = _userRepository.GetUserByUsername(deal.Trader);
            seller.Stack.Cards.Add(tradingCard);
            buyer.Stack.Cards.Remove(tradingCard);
            buyer.Stack.Cards.Add(deal.Card);
            Console.WriteLine("Seller");
            seller.Stack.ToUniversalCardList().ForEach(card => Console.WriteLine(card.Id));
            Console.WriteLine("Buyer");
            buyer.Stack.ToUniversalCardList().ForEach(card => Console.WriteLine(card.Id));
            return _userRepository.AcceptTrade(tradeId, seller, buyer);
        }

        public bool DeleteTrade(string username, string tradeId)
        {
            var trade = _userRepository.GetTrade(tradeId);
            var user = _userRepository.GetUserByUsername(username);
            user.Stack.Cards.Add(trade.Card);
            return trade.Trader.Equals(username) && _userRepository.SetStack(user) &&
                   _userRepository.DeleteTrade(tradeId);
        }

        public bool LogoutUser(string token)
        {
            return _userRepository.LogoutUser(token);
        }
    }
}