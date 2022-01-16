using MonsterTradingCardsGameServer.Core.Authentication;

namespace MonsterTradingCardsGameServer.Core.Routing
{
    /// <summary>
    /// Protected route command interface
    /// </summary>
    public interface IProtectedRouteCommand : IRouteCommand
    {
        IIdentity Identity { get; set; }
    }
}