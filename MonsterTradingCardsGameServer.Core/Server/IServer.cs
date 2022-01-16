namespace MonsterTradingCardsGameServer.Core.Server
{
    /// <summary>
    /// Interface for the Server
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Starts server
        /// </summary>
        void Start();
        
        /// <summary>
        /// Stops server
        /// </summary>
        void Stop();
    }
}