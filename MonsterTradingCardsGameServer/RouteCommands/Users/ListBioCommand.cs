using System;
using System.Text;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Core.Routing;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.RouteCommands.Users
{
    public class ListBioCommand: ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;
        private readonly string _username;
        
        public ListBioCommand(IUserManager userManager, string username)
        {
            Console.WriteLine("Username: " + username);
            _userManager = userManager;
            _username = username;
        }
        
        public override Response Execute()
        {
            UserData userdata;
            try
            {
                userdata = _userManager.GetUserData(_username);
            }
            catch (UserNotFoundException)
            {
                userdata = null;
            }
            Console.WriteLine("UserData 2: "+userdata.Bio);

            var response = new Response();
            if (userdata == null)
            {
                response.StatusCode = StatusCode.NotFound;
            }
            else
            {
                response.StatusCode = StatusCode.Ok;
                response.Payload = userdata.Bio;
            }

            return response;

        }
    }
}