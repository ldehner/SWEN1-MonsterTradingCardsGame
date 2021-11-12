namespace MonsterTradingCardsGame
{
    public class User
    {
        public readonly string Username;
        
        public int Coins { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public string Bio { get; set; }
        public Stack Stack { get; set; }
        public Deck Deck { get; set; }
        
        public User(string username, int coins, int[] stats, string bio, Stack stack, Deck deck)
        {
            Username = username;
            Coins = coins;
            Wins = stats[0];
            Losses = stats[1];
            Bio = bio;
            Stack = stack;
            Deck = deck;
        }
    }
}