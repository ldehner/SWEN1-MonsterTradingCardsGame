using System;
using System.Text;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Users
{
    public class ListBioCommand : ProtectedRouteCommand
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

            var response = new Response();
            if (userdata == null)
            {
                response.StatusCode = StatusCode.NotFound;
            }
            else
            {
                var sb = new StringBuilder();
                sb.Append("Name: ");
                sb.Append(userdata.Name);
                sb.Append("\nBio: ");
                sb.Append(userdata.Bio);
                sb.Append("\nImage: ");
                sb.Append(userdata.Image);
                response.Payload = sb.ToString();
                response.StatusCode = StatusCode.Ok;
            }

            return response;
        }
    }
}