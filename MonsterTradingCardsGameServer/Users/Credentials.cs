namespace MonsterTradingCardsGameServer.Users
{
    /// <summary>
    /// User Credentials
    /// </summary>
    public class Credentials
    {
        /// <summary>
        /// Sets the attributes
        /// </summary>
        /// <param name="username">users username</param>
        /// <param name="password"></param>
        public Credentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}