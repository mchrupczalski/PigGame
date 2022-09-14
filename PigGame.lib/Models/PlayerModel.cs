using System;

namespace PigGame.lib.Models
{
    public class PlayerModel
    {
        public string Name { get; }

        /// <summary>
        ///     Creates new Player instance
        /// </summary>
        /// <param name="name">Player name</param>
        /// <exception cref="ArgumentNullException">Thrown if player name not specified</exception>
        public PlayerModel(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        #region Overrides of Object

        /// <inheritdoc />
        public override string ToString() => Name;

        #endregion
    }
}