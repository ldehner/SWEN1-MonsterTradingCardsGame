using MonsterTradingCardsGameServer.Core.Request;

namespace MonsterTradingCardsGameServer.Core.Client
{
    public interface IClient
    {
        public RequestContext ReceiveRequest();
        public void SendResponse(Response.Response response);
    }
}