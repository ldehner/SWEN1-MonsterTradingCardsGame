namespace MonsterTradingCardsGameServer.Core.Request
{
    /// <summary>
    /// all needed http methods
    /// </summary>
    public enum HttpMethod
    {
        Get,
        Post,
        Put,
        Delete,
        Patch
    }

    /// <summary>
    /// parses methods
    /// </summary>
    public static class MethodUtilities
    {
        /// <summary>
        /// turns text into http method
        /// </summary>
        /// <param name="method">method name</param>
        /// <returns>the http method</returns>
        public static HttpMethod GetMethod(string method)
        {
            method = method.ToLower();
            var parsedMethod = method switch
            {
                "get" => HttpMethod.Get,
                "post" => HttpMethod.Post,
                "put" => HttpMethod.Put,
                "delete" => HttpMethod.Delete,
                "patch" => HttpMethod.Patch,
                _ => HttpMethod.Get
            };

            return parsedMethod;
        }
    }
}