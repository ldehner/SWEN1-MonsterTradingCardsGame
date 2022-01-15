namespace MonsterTradingCardsGameServer.Users
{
    /// <summary>
    /// Users Data
    /// </summary>
    public class UserData
    {
        /// <summary>
        /// Sets all attributes
        /// </summary>
        /// <param name="name">users name</param>
        /// <param name="bio">users bio</param>
        /// <param name="icon">users icon</param>
        public UserData(string name, string bio, string icon)
        {
            Name = name;
            Bio = bio;
            Image = icon;
        }

        public string Name { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
    }
}