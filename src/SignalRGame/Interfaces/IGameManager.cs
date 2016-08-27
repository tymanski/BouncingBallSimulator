using System.Collections.Generic;

namespace SignalRGame
{
    /// <summary>
    /// The interface for the main logic of the "ball bouncer" simulator. 
    /// </summary>
    public interface IGameManager
    {
        /// <summary>
        /// Adds new player object to the players colelction.
        /// </summary>
        /// <param name="connectionId">The conenctionId identifying the player.</param>
        /// <param name="player">The player object.</param>
        void AddPlayer(string connectionId, Player player);

        /// <summary>
        /// Returns a copy of all players list.
        /// </summary>
        /// <returns></returns>
        Player GetPlayer(string connectionId);

        /// <summary>
        /// <para>Returns a clone object of the <see cref="Player"/> object.</para>
        /// <para>If the object has to be updated in players colelction, 
        /// method <see cref="GameManager.UpdatePlayer(string, Player)"/> should be called.</para>
        /// </summary>
        /// <param name="connectionId">The conenctionId identifying the player.</param>
        /// <returns>The clone of player object.</returns>
        Dictionary<string, Player> GetPlayers();

        /// <summary>
        /// Removes Player for the players collection.
        /// </summary>
        /// <param name="connectionId">The conenctionId identifying the player.</param>
        void RemovePlayer(string connectionId);

        /// <summary>
        /// Sets the player's draw color.
        /// </summary>
        /// <param name="connectionId">The conenctionId identifying the player.</param>
        /// <param name="colorValue">The value of the color in format #RRGGBB.</param>
        void SetColor(string connectionId, string colorValue);

        /// <summary>
        /// Updates the Player object in the players colelction by setting it to <paramref name="player"/>.
        /// </summary>
        /// <param name="connectionId">The conenctionId identifying the player.</param>
        /// <param name="player">The updated player's obejct.</param>
        void UpdatePlayer(string connectionId, Player player);
    }
}