using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
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
        public readonly string ConnectionString;
        public Dictionary<string, User> activeUsers;

        public InDatabaseUserRepository(string connectionString)
        {
            ConnectionString = connectionString;
            activeUsers = new Dictionary<string, User>();
        }

        public User GetUserByCredentials(string username, string password)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                const string querystring = "SELECT * FROM data WHERE username = @uname";

                conn.Open();

                using (var cmd = new NpgsqlCommand(querystring, conn))
                {
                    cmd.Parameters.Add("@uname", NpgsqlDbType.Varchar).Value = username;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pw = reader["password"].ToString();
                            if (PasswordManager.ComparePasswords(pw, password))
                            {
                                var dbUsername = reader["username"].ToString();
                                var dbStack = JsonConvert.DeserializeObject<Stack>(reader["stack"].ToString());
                                var dbDeck = JsonConvert.DeserializeObject<Deck>(reader["deck"].ToString());
                                var dbStats = JsonConvert.DeserializeObject<Stats>(reader["stats"].ToString());
                                var dbUserdata = JsonConvert.DeserializeObject<UserData>(reader["userdata"].ToString());
                                var user = new User(dbUsername, dbStats, dbUserdata, dbStack, dbDeck);
                                activeUsers.Add(user.Token, user);
                                return user;
                            }
                        }

                        return null;
                    }
                }
            }
        }

        public User GetUserByAuthToken(string authToken)
        {
            if (activeUsers.ContainsKey(authToken))
            {
                Console.WriteLine("yes");
                return activeUsers[authToken];
            }

            return null;
        }

        public UserData GetUserDataByUsername(string username)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                const string querystring = "SELECT userdata FROM data WHERE username = @uname";

                conn.Open();

                using (var cmd = new NpgsqlCommand(querystring, conn))
                {
                    Console.WriteLine(username);
                    cmd.Parameters.Add("@uname", NpgsqlDbType.Varchar).Value = username;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dbUserdata = JsonConvert.DeserializeObject<UserData>(reader["userdata"].ToString());
                            Console.WriteLine("Bio: "+dbUserdata.Bio);
                            return dbUserdata;
                        }

                        return null;
                    }
                }
            }
        }

        public bool InsertUser(User user, string password)
        {
            IDbConnection connection =
                new NpgsqlConnection(ConnectionString);
            connection.Open();
            {
                var command = connection.CreateCommand();
                command.CommandText =
                    @"INSERT INTO data (username, password, stack, deck, stats, userdata) VALUES (@username, @pw, @stack, @deck, @stats, @userdata)";

                var pFID = command.CreateParameter();
                pFID.DbType = DbType.String;
                pFID.ParameterName = "username";
                pFID.Size = 20;
                command.Parameters.Add(pFID);

                var c = command as NpgsqlCommand;

                c.Parameters.Add("pw", NpgsqlDbType.Varchar, 200);
                c.Parameters.Add("stack", NpgsqlDbType.Jsonb);
                c.Parameters.Add("deck", NpgsqlDbType.Jsonb);
                c.Parameters.Add("stats", NpgsqlDbType.Jsonb);
                c.Parameters.Add("userdata", NpgsqlDbType.Jsonb);

                c.Prepare();

                c.Parameters["username"].Value = user.Username;
                c.Parameters["pw"].Value = PasswordManager.CreatePwHash(password);
                c.Parameters["stack"].Value = JsonConvert.SerializeObject(user.Stack);
                c.Parameters["deck"].Value = JsonConvert.SerializeObject(user.Deck);
                c.Parameters["stats"].Value = JsonConvert.SerializeObject(user.Stats);
                c.Parameters["userdata"].Value = JsonConvert.SerializeObject(user.UserData);
                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}