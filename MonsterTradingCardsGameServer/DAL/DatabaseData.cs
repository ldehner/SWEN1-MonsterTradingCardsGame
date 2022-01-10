namespace MonsterTradingCardsGameServer.DAL
{
    public static class DatabaseData
    {
        public const string ConnectionString = "Host="+Host+";Username="+Username+";Password="+Password+";Database="+Database;
        public const string Password = "mysecretpassword";
        public const string Username = "postgres";
        public const string Database = "postgres";
        public const string Host = "localhost";
        public const string UserTable = "data";
        public const string TradeTable = "trades";
        public const string ScoreBoard = "scoreboard";
        
        public const string UserByUsernameCommand = @"SELECT * FROM data WHERE username = @uname";
        public const string NewUserCommand = @"INSERT INTO data (username, password, stack, deck, stats, elo, userdata, coins, battles) VALUES (@username, @pw, @stack, @deck, @stats, @elo, @userdata, @coins, @battles)";
        public const string UpdateUserDataCommand = @"UPDATE data SET userdata = @userdata WHERE username = @username";
        public const string UpdateUserAfterBattle = @"UPDATE data SET stats = @stats, elo = @elo, battles = @battles  WHERE username = @username";
        public const string GetScoreBoard = @"SELECT username, stats FROM data ORDER BY elo DESC";
        public const string GetUserStack = @"SELECT stack FROM data WHERE username = @username";
        public const string GetUserDeck = @"SELECT deck FROM data WHERE username = @username";
        public const string UpdateUserDeck = @"UPDATE data SET deck = @deck WHERE username = @username";
        public const string UpdateUserStack = @"UPDATE data SET stack = @stack WHERE username = @username";
    }
}