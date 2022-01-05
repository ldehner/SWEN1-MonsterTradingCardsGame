namespace MonsterTradingCardsGameServer.Users
{
    public class UserData
    {
        public int Coins { get; set; }
        public string Bio { get; set; }

        public UserData(int coins, string bio)
        {
            Coins = coins;
            Bio = bio;
        }
    }
}