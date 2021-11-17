using System;

namespace MonsterTradingCardsGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new UserManager();
            manager.LoginUser("bla", "123");
            manager.RegisterUser("bla", "123", "fosdfojds");
            Console.WriteLine("Hello World!");
        }
        
    }
}
