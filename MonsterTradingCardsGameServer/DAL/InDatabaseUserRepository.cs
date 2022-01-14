using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;
using Npgsql;
using NpgsqlTypes;
using Newtonsoft.Json;

namespace MonsterTradingCardsGameServer.DAL
{
    public class InDatabaseUserRepository : IUserRepository
    {
        private Dictionary<string, User> _activeUsers;

        public InDatabaseUserRepository()
        {
            _activeUsers = new Dictionary<string, User>();
        }

        public User GetUserByCredentials(string username, string password)
        {
            var user = GetUserByUsername(username);
            if (user is null || !PasswordManager.ComparePasswords(user?.HashedPassword, password)) return null;
            if (!_activeUsers.ContainsKey(user.Token)) _activeUsers.Add(user.Token, user);
            return user;

        }

        public List<Score> GetScoreBoard()
        {
            using (var conn = new NpgsqlConnection(DatabaseData.ConnectionString))
            {
                using (var c = new NpgsqlCommand(DatabaseData.GetScoreBoard, conn))
                {
                    conn.Open();
                    c.Parameters.Add("@admin", NpgsqlDbType.Varchar).Value = "admin";
                    var scoreList = new List<Score>();
                    using (NpgsqlDataReader reader = c.ExecuteReader())
                    {
                        while (reader.Read()) scoreList.Add(new Score(reader["username"].ToString(), JsonConvert.DeserializeObject<Stats>(reader["stats"].ToString())));
                    }
                    return scoreList;
                }
            }
           
        }

        public Stack GetStack(string username)
        {
            using (var conn = new NpgsqlConnection(DatabaseData.ConnectionString))
            {
                using (var c = new NpgsqlCommand(DatabaseData.GetUserStack, conn))
                {
                    conn.Open();
                    c.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = username;
                    List<UniversalCard> temp = null;
                    var stack = new List<Card>();
                    using (NpgsqlDataReader reader = c.ExecuteReader())
                    {
                        while (reader.Read()) temp = JsonConvert.DeserializeObject<List<UniversalCard>>(reader["stack"].ToString());
                    }
                    temp?.ForEach(card => stack.Add(card.ToCard()));
                    return new Stack(stack);
                }
            }

        }

        public Deck GetDeck(string username)
        {
            using (var conn = new NpgsqlConnection(DatabaseData.ConnectionString))
            {
                using (var c = new NpgsqlCommand(DatabaseData.GetUserDeck, conn))
                {
                    conn.Open();
                    c.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = username;
                    List<UniversalCard> temp = null;
                    var deck = new List<Card>();
                    using (NpgsqlDataReader reader = c.ExecuteReader())
                    {
                        while (reader.Read()) temp = JsonConvert.DeserializeObject<List<UniversalCard>>(reader["deck"].ToString());
                    }
                    temp?.ForEach(card => deck.Add(card.ToCard()));
                    return new Deck(deck);
                }
            }
           
            
        }

        public bool SetDeck(string username, Deck deck)
        {
            using (var conn = new NpgsqlConnection(DatabaseData.ConnectionString))
            {
                using (var c = new NpgsqlCommand(DatabaseData.UpdateUserDeck, conn))
                {
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
            }

        }

        public bool AddPackage(string username, List<UniversalCard> package, Guid id)
        {
            using (var conn = new NpgsqlConnection(DatabaseData.ConnectionString))
            {
                using (var c = new NpgsqlCommand(DatabaseData.AddPackageCommand, conn))
                {
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
            }
        }

        public bool AquirePackage(string username, int coins, Stack stack)
        {
            using (var conn = new NpgsqlConnection(DatabaseData.ConnectionString))
            {
                using (var c = new NpgsqlCommand(DatabaseData.AquirePackageCommand, conn))
                {
                    conn.Open();
                    var id = "";
                    var package = new List<UniversalCard>();
                    using (NpgsqlDataReader reader = c.ExecuteReader()){
                        while (reader.Read())
                        {
                            id = reader["id"].ToString();
                            package = JsonConvert.DeserializeObject<List<UniversalCard>>(reader["package"].ToString());
                        }
                    }
                    stack.Cards.ForEach(card => package?.Add(card.ToUniversalCard()));
  
                    return _deletePackage(id) && _updateStack(package, coins, 5,username);
                }
            }
        }

        public bool CreateTrade(string username, Card card, double minDmg, string tradeId, int type)
        {
            using (var conn = new NpgsqlConnection(DatabaseData.ConnectionString))
            {
                using (var c = new NpgsqlCommand(DatabaseData.AddTrade, conn))
                {
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
            }
        }

        public List<TradingOffer> ListTrades()
        {
            using (var conn = new NpgsqlConnection(DatabaseData.ConnectionString))
            {
                using (var c = new NpgsqlCommand(DatabaseData.GetTrades, conn))
                {
                    conn.Open();
                    var offers = new List<TradingOffer>();
                    using (NpgsqlDataReader reader = c.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            offers.Add(DbToTradingOffer(reader));
                        }
                    }
                    return offers;
                }
            }
        }

        private TradingOffer DbToTradingOffer(NpgsqlDataReader reader)
        {
            var id = reader["id"].ToString();
            var username = reader["username"].ToString();
            var card = JsonConvert.DeserializeObject<UniversalCard>(reader["card"].ToString());
            var minDmg = double.Parse(reader["minDmg"].ToString());
            var cardType = int.Parse(reader["cardType"].ToString()) == 1 ? "Monster" : "Spell";
            return new TradingOffer(id,username, card.ToCard().GetCardName(), card.Damage, cardType, minDmg);
        }

        public Trade GetTrade(string tradeId)
        {
            using (var conn = new NpgsqlConnection(DatabaseData.ConnectionString))
            {
                using (var c = new NpgsqlCommand(DatabaseData.TradeById, conn))
                {
                    conn.Open();
                    
                    c.Parameters.Add("@id", NpgsqlDbType.Varchar).Value = tradeId;

                    using (NpgsqlDataReader reader = c.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var username = reader["username"].ToString();
                            var card = JsonConvert.DeserializeObject<UniversalCard>(reader["card"].ToString());
                            var minDmg = double.Parse(reader["minDmg"].ToString());
                            var cardType = int.Parse(reader["cardType"].ToString()) == 1 ? "Monster" : "Spell";
                            return new Trade(username, card.ToCard(), cardType, minDmg);
                        };
                    }
                    return null;
                }
            }
        }

        public bool DeleteTrade(string tradeId)
        {
            return _deleteById(tradeId, DatabaseData.DeleteTradeCommand);
        }

        public bool AcceptTrade(string tradeId, User seller, User buyer)
        {
            var a1 = _deleteTrade(tradeId);
            var a2 = _updateStack(seller.Stack.ToUniversalCardList(), seller.Coins, 0, seller.Username);
            var a3 = _updateStack(buyer.Stack.ToUniversalCardList(), buyer.Coins, 0, buyer.Username);
            return a1 && a2 && a3;

        }

        public bool SetStack(User user)
        {
            return _updateStack(user.Stack.ToUniversalCardList(), user.Coins,0,user.Username);
        }

        public bool LogoutUser(string token)
        {
            if (!_activeUsers.ContainsKey(token)) return false;
            _activeUsers.Remove(token);
            return true;

        }

        public BattleResult GetBattle(string battleId)
        {
            throw new NotImplementedException();
        }

        public List<BattleResult> ListBattles(string username)
        {
            throw new NotImplementedException();
        }

        private bool _deleteTrade(string tradeId)
        {
            return _deleteById(tradeId, DatabaseData.DeleteTradeCommand);
        }

        private bool _deleteById(string id, string command)
        {
            using (var conn = new NpgsqlConnection(DatabaseData.ConnectionString))
            {
                using (var c = new NpgsqlCommand(command, conn))
                {
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
            }
        }
        

        private bool _deletePackage(string id)
        {
            return _deleteById(id, DatabaseData.DeletePackageCommand);
        }

        private bool _updateStack(List<UniversalCard> stack, int coins, int minusCoins, string username)
        {
            using (var conn = new NpgsqlConnection(DatabaseData.ConnectionString))
            {
                using (var c = new NpgsqlCommand(DatabaseData.UpdateUserAfterPackageBuy, conn))
                {
                    conn.Open();
                    c.CommandText = DatabaseData.UpdateUserAfterPackageBuy;
                    c.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = username;
                    c.Parameters.Add("stack", NpgsqlDbType.Jsonb);
                    c.Parameters.Add("coins", NpgsqlDbType.Integer);
                    c.Prepare();
                    c.Parameters["stack"].Value = JsonConvert.SerializeObject(stack);
                    c.Parameters["coins"].Value = coins-minusCoins;
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

        public User GetUser(NpgsqlDataReader reader)
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
                var user = new User(dbUsername, dbStats, dbUserdata,new Stack(stackList), new Deck(deckList), coins)
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

        public User GetUserByAuthToken(string authToken)
        {
            return _activeUsers.ContainsKey(authToken) ? _activeUsers[authToken] : null;
        }

        public User GetUserByUsername(string username)
        {
            using (var conn = new NpgsqlConnection(DatabaseData.ConnectionString))
            {
                using (var c = new NpgsqlCommand(DatabaseData.UserByUsernameCommand, conn))
                {
                    conn.Open();
                    c.Parameters.Add("@uname", NpgsqlDbType.Varchar).Value = username;
                    User user = null;
                    using (NpgsqlDataReader reader = c.ExecuteReader())
                    {
                        while (reader.Read())
                            user = GetUser(reader);
                    }
                    return user;

                }
            }
            
        }

        public bool UpdateUserData(string username, UserData userData)
        {
            using (var conn = new NpgsqlConnection(DatabaseData.ConnectionString))
            {
                using (var c = new NpgsqlCommand(DatabaseData.UpdateUserDataCommand, conn))
                {
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
            }
        }

        public bool InsertUser(User user, string password)
        {
            using (var conn = new NpgsqlConnection(DatabaseData.ConnectionString))
            {
                using (var c = new NpgsqlCommand(DatabaseData.NewUserCommand, conn))
                {
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
            }
        }
    }
}