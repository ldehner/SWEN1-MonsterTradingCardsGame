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
    ///     Stores the users data in a postgresql database
    /// </summary>
    public class InDatabaseUserRepository : IUserRepository
    {
        private readonly Dictionary<string, User> _activeUsers;

        /// <summary>
        ///     Creates the new repository
        /// </summary>
        public InDatabaseUserRepository()
        {
            _activeUsers = new Dictionary<string, User>();
            _activeUsers.Add("admin-mtcgToken", new User("admin", null, null, null, null, 0));
        }

        /// <summary>
        ///     Gets user out of db with credentials and compares password
        /// </summary>
        /// <param name="username">wanted user</param>
        /// <param name="password">provided password</param>
        /// <returns></returns>
        public User GetUserByCredentials(string username, string password)
        {
            var user = GetUserByUsername(username);
            if (user is null || !PasswordManager.ComparePasswords(user.HashedPassword, password)) return null;

            if (!_activeUsers.ContainsKey(user.Token)) _activeUsers.Add(user.Token, user);


            return user;
        }

        /// <summary>
        ///     Gets all users scores
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
                    JsonConvert.DeserializeObject<Stats>(reader["stats"].ToString() ?? string.Empty)));
            return scoreList;
        }

        /// <summary>
        ///     deletes user out of active users
        /// </summary>
        /// <param name="token">token of the user</param>
        /// <returns>if query was successful</returns>
        public bool LogoutUser(string token)
        {
            if (!_activeUsers.ContainsKey(token)) throw new UserNotFoundException();
            _activeUsers.Remove(token);


            return true;
        }

        /// <summary>
        ///     Gets an user by his auth token if he has signed in
        /// </summary>
        /// <param name="authToken">auth token of the user</param>
        /// <returns>the user</returns>
        public User GetUserByAuthToken(string authToken)
        {
            return _activeUsers.ContainsKey(authToken) ? _activeUsers[authToken] : null;
        }

        /// <summary>
        ///     Gets the user by his username
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
        ///     Updates bio, name and icon of the user
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
        ///     Inserts a user into the database
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
        ///     Reads users data out ouf datareader
        /// </summary>
        /// <param name="reader">the data reader</param>
        /// <returns>the user</returns>
        private static User _getUser(IDataRecord reader)
        {
            try
            {
                var dbUsername = reader["username"].ToString();
                var dbStack =
                    JsonConvert.DeserializeObject<List<UniversalCard>>(reader["stack"].ToString() ?? string.Empty);
                var dbDeck =
                    JsonConvert.DeserializeObject<List<UniversalCard>>(reader["deck"].ToString() ?? string.Empty);
                var dbStats = JsonConvert.DeserializeObject<Stats>(reader["stats"].ToString() ?? string.Empty);
                var dbUserdata = JsonConvert.DeserializeObject<UserData>(reader["userdata"].ToString() ?? string.Empty);
                var coins = int.Parse(reader["coins"].ToString() ?? string.Empty);
                var pw = reader["password"].ToString();
                var battles =
                    JsonConvert.DeserializeObject<List<BattleResult>>(reader["battles"].ToString() ?? string.Empty);
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
            catch (Exception)
            {
                return null;
            }
        }
    }
}