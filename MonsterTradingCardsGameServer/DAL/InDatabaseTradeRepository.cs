using System.Collections.Generic;
using System.Data;
using MonsterTradingCardsGameServer.Cards;
using MonsterTradingCardsGameServer.Trades;
using MonsterTradingCardsGameServer.Users;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;

namespace MonsterTradingCardsGameServer.DAL
{
    /// <summary>
    ///     Makes trade querys
    /// </summary>
    public class InDatabaseTradeRepository : ITradeRepository
    {
        private readonly ICardRepository _cardRepository;

        /// <summary>
        ///     Sets card repository
        /// </summary>
        /// <param name="cardRepository">the card repository</param>
        public InDatabaseTradeRepository(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        /// <summary>
        ///     Creates a new trade offer
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
        ///     Lists all trading offers
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
        ///     Gets a specific trade
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
                var card = JsonConvert.DeserializeObject<UniversalCard>(reader["card"].ToString() ?? string.Empty);
                var minDmg = double.Parse(reader["minDmg"].ToString() ?? string.Empty);
                var cardType = int.Parse(reader["cardType"].ToString() ?? string.Empty) == 1 ? "Monster" : "Spell";
                return new UniversalTrade(username, card?.ToCard(), cardType, minDmg);
            }

            return null;
        }

        /// <summary>
        ///     deletes a trade
        /// </summary>
        /// <param name="tradeId">id of the trade</param>
        /// <returns>if query was successful</returns>
        public bool DeleteTrade(string tradeId)
        {
            using var conn = new NpgsqlConnection(DatabaseData.ConnectionString);
            using var c = new NpgsqlCommand(DatabaseData.DeleteTradeCommand, conn);
            conn.Open();
            c.Parameters.Add("@id", NpgsqlDbType.Varchar).Value = tradeId;

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
        ///     deletes the offer, and updates both users stacks
        /// </summary>
        /// <param name="tradeId">id of the trade</param>
        /// <param name="seller">seller</param>
        /// <param name="buyer">buyer</param>
        /// <returns>if query was successful</returns>
        public bool AcceptTrade(string tradeId, User seller, User buyer)
        {
            var a1 = DeleteTrade(tradeId);
            var a2 = _cardRepository.UpdateStack(seller.Stack.ToUniversalCardList(), seller.Coins, 0, seller.Username);
            var a3 = _cardRepository.UpdateStack(buyer.Stack.ToUniversalCardList(), buyer.Coins, 0, buyer.Username);
            return a1 && a2 && a3;
        }

        /// <summary>
        ///     Converts result of reader into a trading offer
        /// </summary>
        /// <param name="reader">Data reader</param>
        /// <returns>the trading offer</returns>
        private static ReadableTrade _dbToTradingOffer(IDataRecord reader)
        {
            var id = reader["id"].ToString();
            var username = reader["username"].ToString();
            var card = JsonConvert.DeserializeObject<UniversalCard>(reader["card"].ToString() ?? string.Empty);
            var minDmg = double.Parse(reader["minDmg"].ToString() ?? string.Empty);
            var cardType = int.Parse(reader["cardType"].ToString() ?? string.Empty) == 1 ? "Monster" : "Spell";
            return new ReadableTrade(id, username, card?.ToCard().GetCardName(), card!.Damage, cardType, minDmg);
        }
    }
}