using System;

namespace SignalRGame
{
    /// <summary>
    /// Represents a participant in the game simulator.
    /// </summary>
    public class Player : ICloneable
    {
        /// <summary>
        /// Default value of the ball's color.
        /// </summary>
        private static string defaultDrawColor = "#0000ff";

        /// <summary>
        /// Player's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Player's identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The filling color og the ball representing player.
        /// </summary>
        public string DrawColor { get; set; }

        /// <summary>
        /// Constructor (private)
        /// </summary>
        private Player()
        {
            Id = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            DrawColor = defaultDrawColor;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The player's name.</param>
        public Player(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        /// Creates a shallow copy of the object.
        /// </summary>
        /// <returns>Clonend object.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}