using System;
using System.Data;
using MonsterTradingCardsGameServer.Battles;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;

namespace MonsterTradingCardsGameServer.DAL
{
    public class InDatabaseBattleRepository : IBattleRepository
    {
        private readonly IDbConnection _connection;

        public InDatabaseBattleRepository()
        {
            _connection = new NpgsqlConnection(DatabaseData.ConnectionString);
            _connection.Open();
        }
        
        public bool NewBattle(User user)
        {
            var command = _connection.CreateCommand();
            command.CommandText = DatabaseData.UpdateUserAfterBattle;

            var c = command as NpgsqlCommand;

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