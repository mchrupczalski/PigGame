using System;
using System.Collections.Generic;
using System.Linq;

namespace PigGame.lib.Models
{
    public class TurnModel
    {
        #region Delegates

        public delegate DiceRollModel DiceRollModelCreator();

        #endregion

        #region Fields

        private readonly DiceRollModelCreator _createDiceRoll;

        /// <summary>
        ///     Indicates if <see cref="CurrentTurnRolls" /> is active."/>
        /// </summary>
        private bool _turnActive;

        #endregion

        #region Properties

        /// <summary>
        ///     Counts turns
        /// </summary>
        public int TurnCounter { get; private set; }

        /// <summary>
        ///     Holds a list of Rolls for each Turn
        /// </summary>
        public Dictionary<int, DiceRollModel> TurnsRolls { get; } = new Dictionary<int, DiceRollModel>();

        /// <summary>
        ///     The current Turn Rolls
        /// </summary>
        public DiceRollModel CurrentTurnRolls { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates new instance of <see cref="TurnModel" />
        /// </summary>
        /// <param name="diceRollCreator">Factory Method to create <see cref="DiceRollModel" /></param>
        /// <exception cref="ArgumentNullException">Thrown if Factory Method not provided</exception>
        public TurnModel(DiceRollModelCreator diceRollCreator)
        {
            _createDiceRoll = diceRollCreator ?? throw new ArgumentNullException(nameof(diceRollCreator));
        }

        #endregion

        /// <summary>
        ///     Increments the turn counter and sets a new list to store rolls
        /// </summary>
        public void NextTurn()
        {
            TurnCounter++;
            _turnActive = true;

            CurrentTurnRolls = _createDiceRoll();
            CurrentTurnRolls.MustRoll = true;

            TurnsRolls.Add(TurnCounter, CurrentTurnRolls);
        }

        /// <summary>
        ///     Ends current turn
        /// </summary>
        public void EndTurn() => _turnActive = false;

        public void ResetTurns()
        {
            TurnCounter = 0;
            CurrentTurnRolls = null;
            TurnsRolls.Clear();
        }
        /// <summary>
        ///     Sum of all rolls in all turns
        /// </summary>
        /// <returns>
        ///     If a turn is active (player still rolls), returns a sum of all rolls in all turns, but current.
        ///     If current turn ended, returns a sum of all rolls in all turns.
        /// </returns>
        public int GameScore(bool includeCurrentTurn = false)
        {
            if (includeCurrentTurn || !_turnActive)
                return TurnsRolls.Sum(d => d.Value.RollsScore());

            return TurnsRolls.Where(t => t.Value != CurrentTurnRolls)
                             .Sum(d => d.Value.RollsScore());
        }
    }
}