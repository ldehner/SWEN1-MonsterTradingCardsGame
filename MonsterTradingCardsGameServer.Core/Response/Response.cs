namespace MonsterTradingCardsGameServer.Core.Response
{
    /// <summary>
    /// Response, which contains status code and payload
    /// </summary>
    public class Response
    {
        public StatusCode StatusCode { get; set; }
        public string Payload { get; set; }
    }
}