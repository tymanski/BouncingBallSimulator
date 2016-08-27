using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRGame
{
    /// <summary>
    /// The SignalR hub interface for "Bouncing ball" simulator.
    /// </summary>
    public interface IGameHub
    {
        /// <summary>
        /// SignalR context object.
        /// </summary>
        HubCallerContext CurrentContext { get; set; }

        /// <summary>
        /// Connection handling logic.
        /// </summary>
        /// <returns></returns>
        Task OnConnected();

        /// <summary>
        /// Disconnection handling logic.
        /// </summary>
        /// <returns></returns>
        Task OnDisconnected(bool stopCalled);

        /// <summary>
        /// Resconnection handling logic.
        /// </summary>
        /// <returns></returns>
        Task OnReconnected();

        /// <summary>
        /// Broadcasts messages to all suers.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        void Send(string message);

        /// <summary>
        /// Sets the color for the user.
        /// </summary>
        /// <param name="colorValue">The color value.</param>
        void SetColor(string colorValue);
    }
}