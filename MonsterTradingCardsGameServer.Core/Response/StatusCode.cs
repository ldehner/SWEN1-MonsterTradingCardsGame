namespace MonsterTradingCardsGameServer.Core.Response
{
    /// <summary>
    /// HTTP Status codes
    /// </summary>
    public enum StatusCode
    {
        Ok = 200,
        Created = 201,
        Accepted = 202,
        NoContent = 204,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
        InternalServerError = 500,
        NotImplemented = 501
    }
}