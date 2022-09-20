using System;

namespace PigGame.lib.Models
{
    public class PlayerModel
    {
        #region Properties

        /// <summary>
        ///     Player name
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Holds info about player turns and rolls
        /// </summary>
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
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
            Turns = playerTurns ?? throw new ArgumentNullException(nameof(playerTurns));
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => Name;

        #endregion
    }
}