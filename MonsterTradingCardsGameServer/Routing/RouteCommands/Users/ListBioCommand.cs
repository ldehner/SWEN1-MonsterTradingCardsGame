using System;
using System.Text;
using MonsterTradingCardsGameServer.Core.Response;
using MonsterTradingCardsGameServer.Manager;
using MonsterTradingCardsGameServer.Users;

namespace MonsterTradingCardsGameServer.Routing.RouteCommands.Users
{
    /// <summary>
    /// Lists a specific users userdata
    /// </summary>
    public class ListBioCommand : ProtectedRouteCommand
    {
        private readonly IUserManager _userManager;
        private readonly string _username;

        /// <summary>
        /// Sets user manager and username
        /// </summary>
        /// <param name="userManager">the user manager</param>
        /// <param name="username">the user which user data is wanted</param>
        public ListBioCommand(IUserManager userManager, string username)
        {
            _userManager = userManager;
            _username = username;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <returns>the response in form of status code and payload</returns>
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