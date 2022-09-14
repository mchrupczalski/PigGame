using System;

namespace PigGame.lib.Models
{
    public class PlayerModel
    {
        #region Properties

        public GameScoreModel Game { get; }
        public string Name { get; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates new Player instance
        /// </summary>
        /// <param name="name">Player name</param>
        /// <param name="gameScoreModel">Game Model keeping user track of player turns, throws and score</param>
        /// <exception cref="ArgumentNullException">Thrown if player name not specified</exception>
        public PlayerModel(string name, GameScoreModel gameScoreModel)
        {
            Game = gameScoreModel;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => Name;

        #endregion
    }
}