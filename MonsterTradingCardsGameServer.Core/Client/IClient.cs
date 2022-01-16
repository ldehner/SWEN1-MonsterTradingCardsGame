using MonsterTradingCardsGameServer.Core.Request;

namespace MonsterTradingCardsGameServer.Core.Client
{
    /// <summary>
    /// interface for client
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// recieves users request
        /// </summary>
        /// <returns>the request context</returns>
        public RequestContext ReceiveRequest();
        
        /// <summary>
        /// Sends response back to client
        /// </summary>
        /// <param name="response">requests response</param>
        public void SendResponse(Response.Response response);
    }
}