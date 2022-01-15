using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;

namespace MonsterTradingCardsGameServer.DAL
{
    /// <summary>
    /// Stores the battles in a postgresql database
    /// </summary>
    public class InDatabaseBattleRepository : IBattleRepository
    {
        /// <summary>
        /// stores a new battle into the calling users row
        /// </summary>
        /// <param name="user">requesting user</param>
        /// <returns>if request was successful</returns>
        public bool NewBattle(User user)
        {
            using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
            using var c = new NpgsqlCommand(DatabaseData.UpdateUserAfterBattle, conn);
            conn.Open();
            c.Parameters.Add("stats", NpgsqlDbType.Jsonb);
            c.Parameters.Add("battles", NpgsqlDbType.Jsonb);
            c.Parameters.Add("elo", NpgsqlDbType.Integer);
            c.Parameters.Add("username", NpgsqlDbType.Varchar).Value = user.Username;

            c.Prepare();

            c.Parameters["stats"].Value = JsonConvert.SerializeObject(user.Stats);
            c.Parameters["battles"].Value = JsonConvert.SerializeObject(user.Battles);
            c.Parameters["elo"].Value = user.Stats.Elo;
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