namespace MonsterTradingCardsGameServer.Core.Routing
{
    /// <summary>
    ///     Route command interface
    /// </summary>
    public interface IRouteCommand
    {
        /// <summary>
        ///     Executes the command
        /// </summary>
        /// <returns>commands response</returns>
        Response.Response Execute();
    }
}