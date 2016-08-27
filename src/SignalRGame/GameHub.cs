using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRGame
{
    /// <summary>
    /// The SignalR hub class for "Bouncing ball" simulator.
    /// </summary>
    public class GameHub : Hub, IGameHub
    {
        /// <summary>
        /// Object containing the core logic.
        /// </summary>
        private readonly static IGameManager _gameManager =
            new GameManager();

        /// <summary>
        /// SignalR context object.
        /// </summary>
        private HubCallerContext _context;

        /// <summary>
        /// SignalR Clients object.
        /// </summary>
        private IHubCallerConnectionContext<dynamic> _clients;

        /// <summary>
        /// SignalR context object.
        /// </summary>
        public HubCallerContext CurrentContext
        {
            get
            {
                return _context == null ? Context : _context;
            }
            set
            {
                _context = value;
            }
        }

        /// <summary>
        /// SignalR Clients object.
        /// </summary>
        public IHubCallerConnectionContext<dynamic> CurrentClients
        {
            get
            {
                return _clients == null ? Clients : _clients;
            }
            set
            {
                _clients = value;
            }
        }

        /// <summary>
        /// Connection handling logic.
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            string name = PrepareName();

            // Add new player to the game
            _gameManager.AddPlayer(CurrentContext.ConnectionId, new Player(name));

            // Send messages to all
            CurrentClients.Caller.addNewMessageToPage("System: ","Welcome! You are in the game now...", "system");
            CurrentClients.Others.addNewMessageToPage("System: ", "User '"+ name + "' joined!", "system");

            // Send players
            CurrentClients.Caller.initializePlayers(_gameManager.GetPlayers().Select(x => x.Value).ToArray());

            // Send info about new player to others
            CurrentClients.Others.addPlayer(_gameManager.GetPlayer(CurrentContext.ConnectionId));

            return base.OnConnected();
        }

        /// <summary>
        /// Disconnection handling logic.
        /// </summary>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            Player player = _gameManager.GetPlayer(CurrentContext.ConnectionId);

            if (player != null)
            {
                _gameManager.RemovePlayer(CurrentContext.ConnectionId);

                CurrentClients.All.addNewMessageToPage("System: ", "'" + player.Name + "' left the game..", "system");
                CurrentClients.All.removePlayer(player.Id);
            }

            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// Resconnection handling logic.
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            Player player = _gameManager.GetPlayer(CurrentContext.ConnectionId);

            CurrentClients.Caller.addNewMessageToPage("System", "Welcome back!", "system");
            CurrentClients.Others.addNewMessageToPage("System", "'" + player.Name + "' is back!", "system");

            // Send players
            CurrentClients.Caller.initializePlayers(_gameManager.GetPlayers());

            return base.OnReconnected();
        }

        /// <summary>
        /// Broadcasts messages to all suers.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void Send(string message)
        {
            string messageEscaped = System.Security.SecurityElement.Escape(message).Trim();

            var player = _gameManager.GetPlayer(CurrentContext.ConnectionId);

            if (player != null)
            {
                if (!String.IsNullOrEmpty(messageEscaped))
                {
                    CurrentClients.All.addNewMessageToPage(player.Name, messageEscaped);
                }
            }
        }

        /// <summary>
        /// Sets the color for the user.
        /// </summary>
        /// <param name="colorValue">The color value.</param>
        public void SetColor(string colorValue)
        {
            _gameManager.SetColor(CurrentContext.ConnectionId, colorValue);

            CurrentClients.All.updatePlayer(_gameManager.GetPlayer(CurrentContext.ConnectionId));
        }

        /// <summary>
        /// PErforms essential operation on the provided Name string in order to set it in the system. 
        /// If the name is not provided, the defautl value is set.
        /// </summary>
        /// <returns></returns>
        private string PrepareName()
        {
            string name = "Anonymous";

            if (CurrentContext.QueryString != null
                && !String.IsNullOrEmpty(CurrentContext.QueryString["name"]))
            {
                string nameRaw = CurrentContext.QueryString["name"].Trim();

                if (nameRaw.Length > 15)
                {
                    name = nameRaw.Substring(0, 15);
                }
                else
                {
                    name = nameRaw;
                }
            }

            return name;
        }

    }
}
