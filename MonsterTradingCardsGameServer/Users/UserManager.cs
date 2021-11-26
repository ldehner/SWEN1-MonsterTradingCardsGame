using System;
using System.Collections.Generic;

namespace MonsterTradingCardsGameServer.Users
{
    public class UserManager
    {
        public Dictionary<string, User> users { get; set; }
        public UserManager()
        {
            users = new Dictionary<string, User>();
        }
        /**
         Logs the user in
         */
        public bool LoginUser(string username, string password)
        {
            var user = DatabaseConnector.ValidateUser(username, password);
            if (user != null)
            {
                users.Add(GenerateToken(username), user); 
                return true;
            }
            return false;
        }

        public string TempLogin(User user)
        {
            var token = GenerateToken(user.Username);
            users.Add(token, user);
            return token;
        }

        public bool RegisterUser(string username, string password, string bio)
        {
            var user = DatabaseConnector.RegisterUser(username, password, bio);
            if (user != null) return true;
            return false;
        }

        public string GenerateToken(string username)
        {
            var rand = new Random();
            return $"mtcg-{username}-{rand.Next(9999)}";
        }
    }
}