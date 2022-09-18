using System;
using PigGame.lib.Models;

namespace PigGame.lib.Factories
{
    public class PlayerFactory
    {
        #region Delegates

        public delegate TurnModel TurnCreator();

        #endregion

        #region Fields

        private readonly TurnCreator _createTurn;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates new instance of <see cref="PlayerFactory" />
        /// </summary>
        /// <param name="createTurn">Factory Method to create <see cref="TurnModel" /></param>
        /// <exception cref="ArgumentNullException">Thrown if factory method not provided</exception>
        public PlayerFactory(TurnCreator createTurn)
        {
            _createTurn = createTurn ?? throw new ArgumentNullException(nameof(createTurn));
        }

        #endregion

        /// <summary>
        ///     Creates a new Player object
        /// </summary>
        /// <param name="name">Player name</param>
        /// <returns></returns>
        public PlayerModel Create(string name) => new PlayerModel(name, _createTurn());
    }
}