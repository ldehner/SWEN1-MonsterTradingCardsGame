using System;
using System.Collections.Generic;

namespace MonsterTradingCardsGame
{
    public class UserManager
    {
        public Dictionary<Guid, User> users { get; set; }
        public UserManager()
        {
            users = new Dictionary<Guid, User>();
        }
        /**
         Logs the user in
         */
        public bool LoginUser(string username, string password)
        {
            var user = DatabaseConnector.ValidateUser(username, password);
            if (user != null)
            {
                users.Add(Guid.NewGuid(), user); 
                return true;
            }
            return false;
        }

        public bool RegisterUser(string username, string password, string bio)
        {
            var user = DatabaseConnector.RegisterUser(username, password, bio);
            if (user != null) return true;
            return false;
        }
    }
}