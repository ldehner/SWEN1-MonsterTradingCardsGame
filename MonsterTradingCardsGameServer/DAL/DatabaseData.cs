namespace MonsterTradingCardsGameServer.DAL
{
    /// <summary>
    ///     Inherits all the neccessarry data for the db
    /// </summary>
    public static class DatabaseData
    {
        public const string ConnectionString = "Host=" + Host + ";Username=" + Username + ";Password=" + Password +
                                               ";Database=" + Database;

        private const string Password = "mysecretpassword";
        private const string Username = "postgres";
        private const string Database = "postgres";
        private const string Host = "localhost";
        private const string UserTable = "data";
        private const string TradeTable = "trades";
        private const string PackageTable = "packages";

        public const string UserByUsernameCommand = @"SELECT * FROM " + UserTable + " WHERE username = @uname";

        public const string NewUserCommand = @"INSERT INTO " + UserTable +
                                             " (username, password, stack, deck, stats, elo, userdata, coins, battles) VALUES (@username, @pw, @stack, @deck, @stats, @elo, @userdata, @coins, @battles)";

        public const string UpdateUserDataCommand =
            @"UPDATE " + UserTable + " SET userdata = @userdata WHERE username = @username";

        public const string UpdateUserAfterBattle = @"UPDATE " + UserTable +
                                                    " SET stats = @stats, elo = @elo, battles = @battles  WHERE username = @username";

        public const string GetScoreBoard =
            @"SELECT username, stats FROM " + UserTable + " WHERE username <> @admin ORDER BY elo DESC";

        public const string GetUserStack = @"SELECT stack FROM " + UserTable + " WHERE username = @username";
        public const string GetUserDeck = @"SELECT deck FROM " + UserTable + " WHERE username = @username";
        public const string UpdateUserDeck = @"UPDATE " + UserTable + " SET deck = @deck WHERE username = @username";

        public const string AddPackageCommand =
            @"INSERT INTO " + PackageTable + " (id, package, counter) VALUES (@id, @package, DEFAULT)";

        public const string DeletePackageCommand = @"DELETE FROM " + PackageTable + " WHERE id = @id";
        public const string AquirePackageCommand = @"SELECT * FROM " + PackageTable + " ORDER BY counter ASC LIMIT 1";

        public const string UpdateUserAfterPackageBuy =
            @"UPDATE " + UserTable + " SET stack = @stack, coins = @coins WHERE username = @username";

        public const string AddTrade = @"INSERT INTO " + TradeTable +
                                       " (id, username, card, minDmg, cardType) VALUES (@id, @username, @card, @minDmg, @cardType)";

        public const string GetTrades = @"SELECT * FROM " + TradeTable + "";
        public const string TradeById = @"SELECT * FROM " + TradeTable + " WHERE id = @id";
        public const string DeleteTradeCommand = @"DELETE FROM " + TradeTable + " WHERE id = @id";
        public static object TradeLock = new();
        public static object UserLock = new();
        public static object PackageLock = new();
        public static object ActiveUserLock = new();
    }
}