using System;
using System.Collections.Generic;
using System.Data;
using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;

namespace MonsterTradingCardsGameServer.DAL
{
    /// <summary>
    /// Stores the users data in a postgresql database
    /// </summary>
    public class InDatabaseUserRepository : IUserRepository
    {
        private readonly Dictionary<string, User> _activeUsers;

        /// <summary>
        /// Creates the new repository
        /// </summary>
        public InDatabaseUserRepository()
        {
            _activeUsers = new Dictionary<string, User>();
        }

        /// <summary>
        /// Gets user out of db with credentials and compares password
        /// </summary>
        /// <param name="username">wanted user</param>
        /// <param name="password">provided password</param>
        /// <returns></returns>
        public User GetUserByCredentials(string username, string password)
        {
            var user = GetUserByUsername(username);
            if (user is null || !PasswordManager.ComparePasswords(user?.HashedPassword, password)) return null;
            if (!_activeUsers.ContainsKey(user.Token)) _activeUsers.Add(user.Token, user);
            return user;
        }

        /// <summary>
        /// Gets all users scores
        /// </summary>
        /// <returns>Returns a list of Scores if successful</returns>
        public List<Score> GetScoreBoard()
        {
            using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
            using var c = new NpgsqlCommand(DatabaseData.GetScoreBoard, conn);
            conn.Open();
            c.Parameters.Add("@admin", NpgsqlDbType.Varchar).Value = "admin";
            var scoreList = new List<Score>();
            using var reader = c.ExecuteReader();
            while (reader.Read())
                scoreList.Add(new Score(reader["username"].ToString(),
                    JsonConvert.DeserializeObject<Stats>(reader["stats"].ToString())));

            return scoreList;
        }

        /// <summary>
        /// Gets the stack of the user
        /// </summary>
        /// <param name="username">wanted user</param>
        /// <returns>the stack</returns>
        public Stack GetStack(string username)
        {
            using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
            using var c = new NpgsqlCommand(DatabaseData.GetUserStack, conn);
            conn.Open();
            c.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = username;
            List<UniversalCard> temp = null;
            var stack = new List<Card>();
            using var reader = c.ExecuteReader();
            while (reader.Read())
                temp = JsonConvert.DeserializeObject<List<UniversalCard>>(reader["stack"].ToString());

            temp?.ForEach(card => stack.Add(card.ToCard()));
            return new Stack(stack);
        }

        /// <summary>
        /// Gets the deck of the user
        /// </summary>
        /// <param name="username">wanted user</param>
        /// <returns>deck of the user</returns>
        public Deck GetDeck(string username)
        {
            using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
            using var c = new NpgsqlCommand(DatabaseData.GetUserDeck, conn);
            conn.Open();
            c.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = username;
            List<UniversalCard> temp = null;
            var deck = new List<Card>();
            using var reader = c.ExecuteReader();
            while (reader.Read())
                temp = JsonConvert.DeserializeObject<List<UniversalCard>>(reader["deck"].ToString());

            temp?.ForEach(card => deck.Add(card.ToCard()));
            return new Deck(deck);
        }

        /// <summary>
        /// sets the deck of the user
        /// </summary>
        /// <param name="username">wanted user</param>
        /// <param name="deck">new deck</param>
        /// <returns>if query was successful</returns>
        public bool SetDeck(string username, Deck deck)
        {
            using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
            using var c = new NpgsqlCommand(DatabaseData.UpdateUserDeck, conn);
            conn.Open();
            c.Parameters.Add("deck", NpgsqlDbType.Jsonb);
            c.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = username;

            c.Prepare();

            c.Parameters["deck"].Value = JsonConvert.SerializeObject(deck.ToUniversalCardList());
            try
            {
                c.ExecuteNonQuery();
                return true;
            }
            catch (PostgresException)
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a new package into the db
        /// </summary>
        /// <param name="package">universal card list</param>
        /// <param name="id">uid of the package</param>
        /// <returns>if query was successful</returns>
        public bool AddPackage(List<UniversalCard> package, Guid id)
        {
            using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
            using var c = new NpgsqlCommand(DatabaseData.AddPackageCommand, conn);
            conn.Open();
            c.Parameters.Add("id", NpgsqlDbType.Varchar, 200);
            c.Parameters.Add("package", NpgsqlDbType.Jsonb);

            c.Prepare();

            c.Parameters["id"].Value = id.ToString();
            c.Parameters["package"].Value = JsonConvert.SerializeObject(package);
            try
            {
                c.ExecuteNonQuery();
                return true;
            }
            catch (PostgresException)
            {
                return false;
            }
        }

        /// <summary>
        /// Adds an quicred package into the stack of the user and updates the number of coins
        /// </summary>
        /// <param name="username">wanted user</param>
        /// <param name="coins">users number of coins</param>
        /// <param name="stack">users stack</param>
        /// <returns>if query was successful</returns>
        public bool AquirePackage(string username, int coins, Stack stack)
        {
            using (var conn = new NpgsqlConnection(DatabaseData.ConnectionString))
            {
                using (var c = new NpgsqlCommand(DatabaseData.AquirePackageCommand, conn))
                {
                    conn.Open();
                    var id = "";
                    var package = new List<UniversalCard>();
                    using (var reader = c.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = reader["id"].ToString();
                            package = JsonConvert.DeserializeObject<List<UniversalCard>>(reader["package"].ToString());
                        }
                    }

                    stack.Cards.ForEach(card => package?.Add(card.ToUniversalCard()));

                    return _deletePackage(id) && _updateStack(package, coins, 5, username);
                }
            }
        }

        /// <summary>
        /// Creates a new trade offer
        /// </summary>
        /// <param name="username">traders username</param>
        /// <param name="card">card trader wants to trade</param>
        /// <param name="minDmg">min damage requirement</param>
        /// <param name="tradeId">trade uid</param>
        /// <param name="type">required card type</param>
        /// <returns>if query was successful</returns>
        public bool CreateTrade(string username, Card card, double minDmg, string tradeId, int type)
        {
            using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
            using var c = new NpgsqlCommand(DatabaseData.AddTrade, conn);
            conn.Open();

            c.Parameters.Add("id", NpgsqlDbType.Varchar, 200);
            c.Parameters.Add("username", NpgsqlDbType.Varchar, 200);
            c.Parameters.Add("card", NpgsqlDbType.Jsonb);
            c.Parameters.Add("minDmg", NpgsqlDbType.Double);
            c.Parameters.Add("cardType", NpgsqlDbType.Integer);

            c.Prepare();

            c.Parameters["id"].Value = tradeId;
            c.Parameters["username"].Value = username;
            c.Parameters["card"].Value = JsonConvert.SerializeObject(card.ToUniversalCard());
            c.Parameters["minDmg"].Value = minDmg;
            c.Parameters["cardType"].Value = type;

            try
            {
                c.ExecuteNonQuery();
                return true;
            }
            catch (PostgresException)
            {
                return false;
            }
        }

        /// <summary>
        /// Lists all trading offers
        /// </summary>
        /// <returns>all trading offers</returns>
        public List<ReadableTrade> ListTrades()
        {
            using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
            using var c = new NpgsqlCommand(DatabaseData.GetTrades, conn);
            conn.Open();
            var offers = new List<ReadableTrade>();
            using var reader = c.ExecuteReader();
            while (reader.Read()) offers.Add(_dbToTradingOffer(reader));

            return offers;
        }

        /// <summary>
        /// Gets a specific trade
        /// </summary>
        /// <param name="tradeId">trade id</param>
        /// <returns>the trade</returns>
        public UniversalTrade GetTrade(string tradeId)
        {
            using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
            using var c = new NpgsqlCommand(DatabaseData.TradeById, conn);
            conn.Open();

            c.Parameters.Add("@id", NpgsqlDbType.Varchar).Value = tradeId;

            using var reader = c.ExecuteReader();
            while (reader.Read())
            {
                var username = reader["username"].ToString();
                var card = JsonConvert.DeserializeObject<UniversalCard>(reader["card"].ToString());
                var minDmg = double.Parse(reader["minDmg"].ToString());
                var cardType = int.Parse(reader["cardType"].ToString()) == 1 ? "Monster" : "Spell";
                return new UniversalTrade(username, card.ToCard(), cardType, minDmg);
            }
            return null;
        }

        /// <summary>
        /// deletes a trade
        /// </summary>
        /// <param name="tradeId">id of the trade</param>
        /// <returns>if query was successful</returns>
        public bool DeleteTrade(string tradeId)
        {
            return _deleteById(tradeId, DatabaseData.DeleteTradeCommand);
        }

        /// <summary>
        /// deletes the offer, and updates both users stacks
        /// </summary>
        /// <param name="tradeId">id of the trade</param>
        /// <param name="seller">seller</param>
        /// <param name="buyer">buyer</param>
        /// <returns>if query was successful</returns>
        public bool AcceptTrade(string tradeId, User seller, User buyer)
        {
            var a1 = _deleteTrade(tradeId);
            var a2 = _updateStack(seller.Stack.ToUniversalCardList(), seller.Coins, 0, seller.Username);
            var a3 = _updateStack(buyer.Stack.ToUniversalCardList(), buyer.Coins, 0, buyer.Username);
            return a1 && a2 && a3;
        }

        /// <summary>
        /// Sets the stack of an user
        /// </summary>
        /// <param name="user">wanted user</param>
        /// <returns>if query was successful</returns>
        public bool SetStack(User user)
        {
            return _updateStack(user.Stack.ToUniversalCardList(), user.Coins, 0, user.Username);
        }

        /// <summary>
        /// deletes user out of active users
        /// </summary>
        /// <param name="token">token of the user</param>
        /// <returns>if query was successful</returns>
        public bool LogoutUser(string token)
        {
            if (!_activeUsers.ContainsKey(token)) return false;
            _activeUsers.Remove(token);
            return true;
        }

        /// <summary>
        /// Reads users data out ouf datareader
        /// </summary>
        /// <param name="reader">the data reader</param>
        /// <returns>the user</returns>
        private static User _getUser(IDataRecord reader)
        {
            try
            {
                var dbUsername = reader["username"].ToString();
                var dbStack = JsonConvert.DeserializeObject<List<UniversalCard>>(reader["stack"].ToString());
                var dbDeck = JsonConvert.DeserializeObject<List<UniversalCard>>(reader["deck"].ToString());
                var dbStats = JsonConvert.DeserializeObject<Stats>(reader["stats"].ToString());
                var dbUserdata = JsonConvert.DeserializeObject<UserData>(reader["userdata"].ToString());
                var coins = int.Parse(reader["coins"].ToString());
                var pw = reader["password"].ToString();
                var battles = JsonConvert.DeserializeObject<List<BattleResult>>(reader["battles"].ToString());
                var stackList = new List<Card>();
                var deckList = new List<Card>();
                dbStack?.ForEach(card => stackList.Add(card.ToCard()));
                dbDeck?.ForEach(card => deckList.Add(card.ToCard()));
                var user = new User(dbUsername, dbStats, dbUserdata, new Stack(stackList), new Deck(deckList), coins)
                {
                    HashedPassword = pw,
                    Battles = battles
                };
                return user;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets an user by his auth token if he has signed in
        /// </summary>
        /// <param name="authToken">auth token of the user</param>
        /// <returns>the user</returns>
        public User GetUserByAuthToken(string authToken)
        {
            return _activeUsers.ContainsKey(authToken) ? _activeUsers[authToken] : null;
        }

        /// <summary>
        /// Gets the user by his username
        /// </summary>
        /// <param name="username">username of user</param>
        /// <returns>the user</returns>
        public User GetUserByUsername(string username)
        {
            using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
            using var c = new NpgsqlCommand(DatabaseData.UserByUsernameCommand, conn);
            conn.Open();
            c.Parameters.Add("@uname", NpgsqlDbType.Varchar).Value = username;
            User user = null;
            using var reader = c.ExecuteReader();
            while (reader.Read())
                user = _getUser(reader);

            return user;
        }

        /// <summary>
        /// Updates bio, name and icon of the user
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="userData">new user data</param>
        /// <returns>if query was successful</returns>
        public bool UpdateUserData(string username, UserData userData)
        {
            using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
            using var c = new NpgsqlCommand(DatabaseData.UpdateUserDataCommand, conn);
            conn.Open();
            c.Parameters.Add("userdata", NpgsqlDbType.Jsonb);
            c.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = username;

            c.Prepare();

            c.Parameters["userdata"].Value = JsonConvert.SerializeObject(userData);
            try
            {
                c.ExecuteNonQuery();
                return true;
            }
            catch (PostgresException)
            {
                return false;
            }
        }

        /// <summary>
        /// Inserts a user into the database
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="password">passsword of the user</param>
        /// <returns>if query was successful</returns>
        public bool InsertUser(User user, string password)
        {
            using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
            using var c = new NpgsqlCommand(DatabaseData.NewUserCommand, conn);
            conn.Open();
            var uname = c.CreateParameter();
            uname.DbType = DbType.String;
            uname.ParameterName = "username";
            uname.Size = 20;
            c.Parameters.Add(uname);


            c.Parameters.Add("pw", NpgsqlDbType.Varchar, 200);
            c.Parameters.Add("stack", NpgsqlDbType.Jsonb);
            c.Parameters.Add("deck", NpgsqlDbType.Jsonb);
            c.Parameters.Add("stats", NpgsqlDbType.Jsonb);
            c.Parameters.Add("elo", NpgsqlDbType.Integer);
            c.Parameters.Add("userdata", NpgsqlDbType.Jsonb);
            c.Parameters.Add("coins", NpgsqlDbType.Integer);
            c.Parameters.Add("battles", NpgsqlDbType.Jsonb);

            c.Prepare();

            c.Parameters["username"].Value = user.Username;
            c.Parameters["pw"].Value = PasswordManager.CreatePwHash(password);
            c.Parameters["stack"].Value = JsonConvert.SerializeObject(user.Stack.ToUniversalCardList());
            c.Parameters["deck"].Value = JsonConvert.SerializeObject(user.Deck.ToUniversalCardList());
            c.Parameters["stats"].Value = JsonConvert.SerializeObject(user.Stats);
            c.Parameters["elo"].Value = user.Stats.Elo;
            c.Parameters["userdata"].Value = JsonConvert.SerializeObject(user.UserData);
            c.Parameters["coins"].Value = user.Coins;
            c.Parameters["battles"].Value = JsonConvert.SerializeObject(user.Battles);
            try
            {
                c.ExecuteNonQuery();
                return true;
            }
            catch (PostgresException)
            {
                return false;
            }
        }

        /// <summary>
        /// Converts result of reader into a trading offer
        /// </summary>
        /// <param name="reader">Data reader</param>
        /// <returns>the trading offer</returns>
        private static ReadableTrade _dbToTradingOffer(IDataRecord reader)
        {
            var id = reader["id"].ToString();
            var username = reader["username"].ToString();
            var card = JsonConvert.DeserializeObject<UniversalCard>(reader["card"].ToString()!);
            var minDmg = double.Parse(reader["minDmg"].ToString()!);
            var cardType = int.Parse(reader["cardType"].ToString()!) == 1 ? "Monster" : "Spell";
            return new ReadableTrade(id, username, card?.ToCard().GetCardName(), card!.Damage, cardType, minDmg);
        }

        /// <summary>
        /// Deletes a trade
        /// </summary>
        /// <param name="tradeId"></param>
        /// <returns></returns>
        private static bool _deleteTrade(string tradeId)
        {
            return _deleteById(tradeId, DatabaseData.DeleteTradeCommand);
        }

        /// <summary>
        /// deletes an element by id
        /// </summary>
        /// <param name="id">id of the element</param>
        /// <param name="command">the command</param>
        /// <returns>if query was successful</returns>
        private static bool _deleteById(string id, string command)
        {
            using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
            using var c = new NpgsqlCommand(command, conn);
            conn.Open();
            c.Parameters.Add("@id", NpgsqlDbType.Varchar).Value = id;

            c.Prepare();

            try
            {
                c.ExecuteNonQuery();
                return true;
            }
            catch (PostgresException)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a package
        /// </summary>
        /// <param name="id">id of the package</param>
        /// <returns>if query was successful</returns>
        private static bool _deletePackage(string id)
        {
            return _deleteById(id, DatabaseData.DeletePackageCommand);
        }

        /// <summary>
        /// Updates users stack
        /// </summary>
        /// <param name="stack">new stack</param>
        /// <param name="coins">users coins</param>
        /// <param name="minusCoins">how many coins should be deducted</param>
        /// <param name="username">users username</param>
        /// <returns>if query was successful</returns>
        private static bool _updateStack(List<UniversalCard> stack, int coins, int minusCoins, string username)
        {
            using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
            using var c = new NpgsqlCommand(DatabaseData.UpdateUserAfterPackageBuy, conn);
            conn.Open();
            c.CommandText = DatabaseData.UpdateUserAfterPackageBuy;
            c.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = username;
            c.Parameters.Add("stack", NpgsqlDbType.Jsonb);
            c.Parameters.Add("coins", NpgsqlDbType.Integer);
            c.Prepare();
            c.Parameters["stack"].Value = JsonConvert.SerializeObject(stack);
            c.Parameters["coins"].Value = coins - minusCoins;
            try
            {
                c.ExecuteNonQuery();
                return true;
            }
            catch (PostgresException)
            {
                return false;
            }
        }
    }
}