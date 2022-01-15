namespace MonsterTradingCardsGameServer.Core.Request
{
    public enum HttpMethod
    {
        Get,
        Post,
        Put,
        Delete,
        Patch
    }

    public static class MethodUtilities
    {
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