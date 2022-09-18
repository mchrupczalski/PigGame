using System;

namespace PigGame.lib.Models
{
    public class PlayerModel
    {
        #region Properties

        public string Name { get; }
        public TurnModel Turns { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates new Player instance
        /// </summary>
        /// <param name="name">Player name</param>
        /// <param name="playerTurns">An object representing PLayer Turns and Rolls</param>
        /// <exception cref="ArgumentNullException">Thrown if player name not specified</exception>
        public PlayerModel(string name, TurnModel playerTurns)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Turns = playerTurns ?? throw new ArgumentNullException(nameof(playerTurns));
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => Name;

        #endregion
    }
}