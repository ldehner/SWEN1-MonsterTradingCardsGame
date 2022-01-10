namespace MonsterTradingCardsGameServer.Users
{
    public class UserData
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }

        public UserData(string name, string bio, string icon)
        {
            Name = name;
            Bio = bio;
            Image = icon;
        }
    }
}