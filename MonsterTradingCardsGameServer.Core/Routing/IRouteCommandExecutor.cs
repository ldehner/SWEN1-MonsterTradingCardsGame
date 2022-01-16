namespace MonsterTradingCardsGameServer.Core.Routing
{
    /// <summary>
    /// Route command executor
    /// </summary>
    public interface IRouteCommandExecutor
    {
        Response.Response ExecuteCommand(IRouteCommand command);
    }
}