using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;

namespace MonsterTradingCardsGameServer.DAL
{
    /// <summary>
    ///     Makes card querys
    /// </summary>
    public class InDatabaseCardRepository : ICardRepository
    {
        /// <summary>
        ///     Gets the stack of the user
        /// </summary>
        /// <param name="username">wanted user</param>
        /// <returns>the stack</returns>
        public Stack GetStack(string username)
        {
            lock (DatabaseData.UserLock)
            {
                using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
                using var c = new NpgsqlCommand(DatabaseData.GetUserStack, conn);
                conn.Open();
                c.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = username;
                List<UniversalCard> temp = null;
                var stack = new List<Card>();
                using var reader = c.ExecuteReader();
                while (reader.Read())
                    temp = JsonConvert.DeserializeObject<List<UniversalCard>>(
                        reader["stack"].ToString() ?? string.Empty);

                temp?.ForEach(card => stack.Add(card.ToCard()));
                return new Stack(stack);
            }
        }

        /// <summary>
        ///     Gets the deck of the user
        /// </summary>
        /// <param name="username">wanted user</param>
        /// <returns>deck of the user</returns>
        public Deck GetDeck(string username)
        {
            lock (DatabaseData.UserLock)
            {
                using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
                using var c = new NpgsqlCommand(DatabaseData.GetUserDeck, conn);
                conn.Open();
                c.Parameters.Add("@username", NpgsqlDbType.Varchar).Value = username;
                List<UniversalCard> temp = null;
                var deck = new List<Card>();
                using var reader = c.ExecuteReader();
                while (reader.Read())
                    temp = JsonConvert.DeserializeObject<List<UniversalCard>>(reader["deck"].ToString() ??
                                                                              string.Empty);

                temp?.ForEach(card => deck.Add(card.ToCard()));
                return new Deck(deck);
            }
        }

        /// <summary>
        ///     sets the deck of the user
        /// </summary>
        /// <param name="username">wanted user</param>
        /// <param name="deck">new deck</param>
        /// <returns>if query was successful</returns>
        public bool SetDeck(string username, Deck deck)
        {
            lock (DatabaseData.UserLock)
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
        }

        /// <summary>
        ///     Sets the stack of an user
        /// </summary>
        /// <param name="user">wanted user</param>
        /// <returns>if query was successful</returns>
        public bool SetStack(User user)
        {
            return UpdateStack(user.Stack.ToUniversalCardList(), user.Coins, 0, user.Username);
        }

        /// <summary>
        ///     Updates users stack
        /// </summary>
        /// <param name="stack">new stack</param>
        /// <param name="coins">users coins</param>
        /// <param name="minusCoins">how many coins should be deducted</param>
        /// <param name="username">users username</param>
        /// <returns>if query was successful</returns>
        public bool UpdateStack(List<UniversalCard> stack, int coins, int minusCoins, string username)
        {
            lock (DatabaseData.UserLock)
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
}