using MonsterTradingCardsGameServer.Core.Client;

namespace MonsterTradingCardsGameServer.Core.Listener
{
    public interface IListener
    {
        IClient AcceptClient();
        void Start();
        void Stop();
    }
}