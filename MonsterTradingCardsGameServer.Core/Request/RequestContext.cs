using System.Collections.Generic;

namespace MonsterTradingCardsGameServer.Core.Request
{
    /// <summary>
    /// User requests context
    /// </summary>
    public class RequestContext
    {
        /// <summary>
        /// sets all attributes
        /// </summary>
        public RequestContext()
        {
            Method = HttpMethod.Get;
            ResourcePath = "";
            HttpVersion = "HTTP/1.1";
            Header = new Dictionary<string, string>();
            Payload = null;
        }

        public HttpMethod Method { get; set; }
        public string ResourcePath { get; set; }
        public string HttpVersion { get; set; }
        public Dictionary<string, string> Header { get; set; }
        public string Payload { get; set; }
    }
}