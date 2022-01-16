using MonsterTradingCardsGameServer.Core.Client;

namespace MonsterTradingCardsGameServer.Core.Listener
{
    /// <summary>
    /// interface for the listener
    /// </summary>
    public interface IListener
    {
        IClient AcceptClient();
        void Start();
        void Stop();
    }
}