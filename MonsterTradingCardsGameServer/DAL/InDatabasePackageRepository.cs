using System;
using System.Collections.Generic;
using MonsterTradingCardsGameServer.Cards;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;

namespace MonsterTradingCardsGameServer.DAL
{
    /// <summary>
    /// Makes Package querys
    /// </summary>
    public class InDatabasePackageRepository : IPackageRepository
    {
        private readonly ICardRepository _cardRepository;
        
        /// <summary>
        /// Sets card repository
        /// </summary>
        /// <param name="cardRepository">the card repository</param>
        public InDatabasePackageRepository(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        
        /// <summary>
        /// Adds a new package into the db
        /// </summary>
        /// <param name="package">universal card list</param>
        /// <param name="id">uid of the package</param>
        /// <returns>if query was successful</returns>
        public bool AddPackage(List<UniversalCard> package, Guid id)
        {
            lock (DatabaseData.PackageLock)
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
            var id = "";
            var package = new List<UniversalCard>();
            lock (DatabaseData.PackageLock)
            {
                using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
                using var c = new NpgsqlCommand(DatabaseData.AquirePackageCommand, conn);
                conn.Open();
                
                using var reader = c.ExecuteReader();
                while (reader.Read())
                {
                    id = reader["id"].ToString();
                    package = JsonConvert.DeserializeObject<List<UniversalCard>>(reader["package"].ToString() ?? string.Empty);
                }
            }
            if (id!.Equals("")) throw new NoMorePackagesException();
            stack.Cards.ForEach(card => package?.Add(card.ToUniversalCard()));
            return _deletePackage(id) && _cardRepository.UpdateStack(package, coins, 5, username);
        }
        
        /// <summary>
        /// Deletes a package
        /// </summary>
        /// <param name="id">id of the package</param>
        /// <returns>if query was successful</returns>
        private static bool _deletePackage(string id)
        {
            lock (DatabaseData.PackageLock)
            {
                using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
                using var c = new NpgsqlCommand(DatabaseData.DeletePackageCommand, conn);
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
}