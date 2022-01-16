using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.DAL;

namespace MonsterTradingCardsGameServer.Users
{
    /// <summary>
    /// UserManager
    /// </summary>
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Sets the repository
        /// </summary>
        /// <param name="userRepository"></param>
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            ActiveUsers = new Dictionary<string, User>();
        }

        private Dictionary<string, User> ActiveUsers { get; set; }

        /// <summary>
        /// Loggs in User
        /// </summary>
        /// <param name="credentials">users credentials</param>
        /// <returns>user</returns>
        public User LoginUser(Credentials credentials)
        {
            return _userRepository.GetUserByCredentials(credentials.Username, credentials.Password) ??
                   throw new UserNotFoundException();
        }

        /// <summary>
        /// gets user
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>user</returns>
        public User GetUser(string username)
        {
            return _userRepository.GetUserByUsername(username) ?? throw new UserNotFoundException();
        }

        /// <summary>
        /// gets users data
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>userdata of user</returns>
        public UserData GetUserData(string username)
        {
            return _userRepository.GetUserByUsername(username).UserData ?? throw new UserNotFoundException();
        }

        /// <summary>
        /// changes users data
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="userData">new userdata</param>
        public void EditUserData(string username, UserData userData)
        {
            if (!_userRepository.UpdateUserData(username, userData)) throw new UserNotFoundException();
        }

        /// <summary>
        /// Lists all scores
        /// </summary>
        /// <returns>the score list</returns>
        public List<Score> GetScores()
        {
            return _userRepository.GetScoreBoard();
        }

        /// <summary>
        /// Gets the stack of a user
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>users stack</returns>
        public Stack GetStack(string username)
        {
            return _userRepository.GetStack(username);
        }

        /// <summary>
        /// Gets the deck of a user
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>users deck</returns>
        public Deck GetDeck(string username)
        {
            return _userRepository.GetDeck(username);
        }

        /// <summary>
        /// sets users deck
        /// </summary>
        /// <param name="username">users deck</param>
        /// <param name="ids">list of ids</param>
        /// <returns>if query was successful</returns>
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

        /// <summary>
        /// adds an package
        /// </summary>
        /// <param name="package">list of cards</param>
        /// <returns>if query was successful</returns>
        public bool AddPackage(List<UserRequestCard> package)
        {
            if (package.Count != 5) return false;
            var tmp = new List<UniversalCard>();
            package.ForEach(card => tmp.Add(card.ToUniversalCard()));
            return _userRepository.AddPackage(tmp, Guid.NewGuid());
        }

        /// <summary>
        /// gets users stats
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>users stats</returns>
        public Stats GetUserStats(string username)
        {
            return _userRepository.GetUserByUsername(username).Stats ?? throw new UserNotFoundException();
        }

        /// <summary>
        /// registers user
        /// </summary>
        /// <param name="credentials">users credentials</param>
        public void RegisterUser(Credentials credentials)
        {
            var user = new User(credentials.Username, new Stats(0, 0),
                new UserData(credentials.Username, "Hey, ich bin neu!", ":)"),
                new Stack(new List<Card>()), new Deck(new List<Card>()), 20);
            if (!_userRepository.InsertUser(user, credentials.Password)) throw new DuplicateUserException();
        }

        /// <summary>
        /// aquires an package
        /// </summary>
        /// <param name="username">users username</param>
        /// <returns>if query was successful</returns>
        public bool AquirePackage(string username)
        {
            var user = GetUser(username);
            return user.Coins < 5
                ? throw new TooFewCoinsException()
                : _userRepository.AquirePackage(username, user.Coins, user.Stack);
        }

        /// <summary>
        /// creates a new trading offer
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="userRequestTrade">trading deal</param>
        /// <returns>if query was successful</returns>
        public bool CreateTrade(string username, UserRequestTrade userRequestTrade)
        {
            var user = GetUser(username);
            var cardInDeck = false;
            Card tradingCard = null;
            user.Stack.Cards.ForEach(card =>
            {
                if (card.Id.ToString().Equals(userRequestTrade.CardToTrade)) tradingCard = card;
            });
            user.Deck.Cards.ForEach(card =>
            {
                if (card.Id.ToString().Equals(userRequestTrade.CardToTrade)) cardInDeck = true;
            });
            user.Stack.Cards.Remove(tradingCard);
            return !cardInDeck && tradingCard is not null &&
                   _userRepository.CreateTrade(username, tradingCard, userRequestTrade.MinimumDamage, userRequestTrade.Id,
                       userRequestTrade.Type.Equals("Monster") ? 1 : 0) && _userRepository.SetStack(user);
        }

        /// <summary>
        /// lists all available trades
        /// </summary>
        /// <returns>all trades</returns>
        public List<ReadableTrade> ListTrades()
        {
            return _userRepository.ListTrades();
        }

        /// <summary>
        /// Accepts a trade and trades cards
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="tradeId">id of the trade</param>
        /// <param name="cardId">id of the card to trade</param>
        /// <returns>if query was successful</returns>
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

        /// <summary>
        /// Deletes a specific trade
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="tradeId">id of the trade</param>
        /// <returns>if query was successful</returns>
        public bool DeleteTrade(string username, string tradeId)
        {
            var trade = _userRepository.GetTrade(tradeId);
            var user = _userRepository.GetUserByUsername(username);
            user.Stack.Cards.Add(trade.Card);
            return trade.Trader.Equals(username) && _userRepository.SetStack(user) &&
                   _userRepository.DeleteTrade(tradeId);
        }

        /// <summary>
        /// logs out user
        /// </summary>
        /// <param name="token">users token</param>
        /// <returns>if query was successful</returns>
        public bool LogoutUser(string token)
        {
            return _userRepository.LogoutUser(token);
        }
    }
}