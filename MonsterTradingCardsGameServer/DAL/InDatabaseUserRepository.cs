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
        private readonly IDbConnection _connection;
        private Dictionary<string, User> _activeUsers;

        public InDatabaseUserRepository()
        {
            _connection = new NpgsqlConnection(DatabaseData.ConnectionString);
            _connection.Open();
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
            var command = _connection.CreateCommand();
            command.CommandText = DatabaseData.GetScoreBoard;

            var c = command as NpgsqlCommand;

            using var reader = c.ExecuteReader();
            var scoreList = new List<Score>();
            while (reader.Read()) scoreList.Add(new Score(reader["username"].ToString(), JsonConvert.DeserializeObject<Stats>(reader["stats"].ToString())));

            return scoreList;
        }

        public List<Card> GetStack(string username)
        {
            var command = _connection.CreateCommand();
            command.CommandText = DatabaseData.GetUserStack;

            var c = command as NpgsqlCommand;
            c.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = username;
            using var reader = c.ExecuteReader();
            List<Card> stack = null;
            while (reader.Read()) stack = JsonConvert.DeserializeObject<Stack>(reader["stack"].ToString())?.Cards;
            Console.WriteLine(stack.Count);
            Console.WriteLine("hello");
            return stack;
        }

        public List<Card> GetDeck(string username)
        {
            var command = _connection.CreateCommand();
            command.CommandText = DatabaseData.GetUserDeck;

            var c = command as NpgsqlCommand;
            c.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = username;

            using var reader = c.ExecuteReader();
            List<Card> deck = null;
            while (reader.Read()) deck = JsonConvert.DeserializeObject<Deck>(reader["deck"].ToString())?.Cards;

            return deck;
        }

        public bool SetDeck(string username, List<Card> cards)
        {
            var command = _connection.CreateCommand();
            command.CommandText = DatabaseData.UpdateUserDeck;

            var c = command as NpgsqlCommand;
            
            c.Parameters.Add("deck", NpgsqlDbType.Jsonb);
            c.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = username;

            c.Prepare();

            c.Parameters["deck"].Value = JsonConvert.SerializeObject(cards);
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (PostgresException)
            {
                return false;
            }
        }

        public User GetUser(NpgsqlDataReader reader)
        {
            try
            {
                var dbUsername = reader["username"].ToString();
                var dbStack = JsonConvert.DeserializeObject<Stack>(reader["stack"].ToString());
                var dbDeck = JsonConvert.DeserializeObject<Deck>(reader["deck"].ToString());
                var dbStats = JsonConvert.DeserializeObject<Stats>(reader["stats"].ToString());
                var dbUserdata = JsonConvert.DeserializeObject<UserData>(reader["userdata"].ToString());
                var coins = int.Parse(reader["coins"].ToString());
                var pw = reader["password"].ToString();
                var battles = JsonConvert.DeserializeObject<List<BattleResult>>(reader["battles"].ToString());
                var user = new User(dbUsername, dbStats, dbUserdata, dbStack, dbDeck, coins);
                user.HashedPassword = pw;
                user.Battles = battles;
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
            var command = _connection.CreateCommand();
            command.CommandText = DatabaseData.UserByUsernameCommand;

            var c = command as NpgsqlCommand;

            c.Parameters.Add("@uname", NpgsqlDbType.Varchar).Value = username;

            using var reader = c.ExecuteReader();
            while (reader.Read()) return GetUser(reader);

            return null;
        }

        public bool UpdateUserData(string username, UserData userData)
        {
            var command = _connection.CreateCommand();
            command.CommandText = DatabaseData.UpdateUserDataCommand;

            var c = command as NpgsqlCommand;

            c.Parameters.Add("userdata", NpgsqlDbType.Jsonb);
            c.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = username;

            c.Prepare();

            c.Parameters["userdata"].Value = JsonConvert.SerializeObject(userData);
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (PostgresException)
            {
                return false;
            }
        }

        public bool InsertUser(User user, string password)
        {
            var command = _connection.CreateCommand();
            command.CommandText = DatabaseData.NewUserCommand;

            var uname = command.CreateParameter();
            uname.DbType = DbType.String;
            uname.ParameterName = "username";
            uname.Size = 20;
            command.Parameters.Add(uname);

            var c = command as NpgsqlCommand;

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
            c.Parameters["stack"].Value = JsonConvert.SerializeObject(user.Stack);
            c.Parameters["deck"].Value = JsonConvert.SerializeObject(user.Deck);
            c.Parameters["stats"].Value = JsonConvert.SerializeObject(user.Stats);
            c.Parameters["elo"].Value = user.Stats.Elo;
            c.Parameters["userdata"].Value = JsonConvert.SerializeObject(user.UserData);
            c.Parameters["coins"].Value = user.Coins;
            c.Parameters["battles"].Value = JsonConvert.SerializeObject(user.Battles);
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (PostgresException)
            {
                return false;
            }
        }
    }
}