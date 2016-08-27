using System.Collections.Generic;
using System.Linq;

namespace SignalRGame
{
    /// <summary>
    /// Class representing the main logic of the "ball bouncer" simulator. 
    /// </summary>
    public class GameManager : IGameManager
    {
        /// <summary>
        /// <para>The dictionary storing collection of players.</para>
        /// <para>The key is conenctionId. The valeu is <see cref="SignalRGame.Player"/> object.</para>
        /// </summary>
        private readonly Dictionary<string, Player> _connections =
            new Dictionary<string, Player>();

        /// <summary>
        /// Adds new player object to the players colelction.
        /// </summary>
        /// <param name="connectionId">The conenctionId identifying the player.</param>
        /// <param name="player">The player object.</param>
        public void AddPlayer(string connectionId, Player player)
        {
            lock (_connections)
            {
                Player val;
                if (!_connections.TryGetValue(connectionId, out val))
                {
                    _connections.Add(connectionId, player);
                }
            }
        }

        /// <summary>
        /// <para>Returns a clone object of the <see cref="Player"/> object.</para>
        /// <para>If the object has to be updated in players colelction, 
        /// method <see cref="GameManager.UpdatePlayer(string, Player)"/> should be called.</para>
        /// </summary>
        /// <param name="connectionId">The conenctionId identifying the player.</param>
        /// <returns>The clone of player object.</returns>
        public Player GetPlayer(string connectionId)
        {
            lock (_connections)
            {
                Player val;
                if (_connections.TryGetValue(connectionId, out val))
                {
                    return (Player)val.Clone();
                }
                else
                {
                    return null;
                }   
            }
        }

        /// <summary>
        /// Updates the Player object in the players colelction by setting it to <paramref name="player"/>.
        /// </summary>
        /// <param name="connectionId">The conenctionId identifying the player.</param>
        /// <param name="player">The updated player's obejct.</param>
        public void UpdatePlayer(string connectionId, Player player)
        {
            lock (_connections)
            {
                Player val;
                if (_connections.TryGetValue(connectionId, out val))
                {
                    _connections[connectionId] = player;
                }
            }
        }

        /// <summary>
        /// Removes Player for the players collection.
        /// </summary>
        /// <param name="connectionId">The conenctionId identifying the player.</param>
        public void RemovePlayer(string connectionId)
        {
            lock (_connections)
            {
                Player val;
                if (!_connections.TryGetValue(connectionId, out val))
                {
                    return;
                }
                _connections.Remove(connectionId);

            }
        }

        /// <summary>
        /// Sets the player's draw color.
        /// </summary>
        /// <param name="connectionId">The conenctionId identifying the player.</param>
        /// <param name="colorValue">The value of the color in format #RRGGBB.</param>
        public void SetColor(string connectionId, string colorValue)
        {
            lock (_connections)
            {
                Player player;
                if (_connections.TryGetValue(connectionId, out player))
                {
                    lock (player)
                    {
                        player.DrawColor = colorValue;
                    }
                }
            }
        }

        /// <summary>
        /// Returns a copy of all players list.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Player> GetPlayers()
        {
            lock (_connections)
            {
                // Returns clone of players dictionary. 
                // This is for thread safety
                return _connections.ToDictionary(entry => entry.Key,
                                               entry => (Player)entry.Value.Clone());
            }
        }
    }
}