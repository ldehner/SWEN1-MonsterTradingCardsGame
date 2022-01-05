namespace MonsterTradingCardsGameServer.Core.Routing
{
    public interface IRouteCommandExecutor
    {
        Response.Response ExecuteCommand(IRouteCommand command);
    }
}
