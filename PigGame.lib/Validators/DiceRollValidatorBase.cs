using System;
using System.Collections.Generic;
using System.Linq;

namespace PigGame.lib.Validators
{
    public abstract class DiceRollValidatorBase : IDiceRollValidator
    {
        #region Properties

        /// <summary>
        ///     List of rolls to validate
        /// </summary>
        protected List<int> Rolls { get; private set; }

        /// <summary>
        ///     Rolled value on Dice One
        /// </summary>
        protected int DiceOne { get; private set; }

        /// <summary>
        ///     Rolled value on Dice Two (can be null if only one dice used for the game)
        /// </summary>
        protected int? DiceTwo { get; private set; }

        #endregion

        #region Interfaces Implement

        /// <inheritdoc />
        public virtual bool RollIsValid(IEnumerable<int> rolls)
        {
            Rolls = rolls.ToList();
            if (Rolls.Count == 0) throw new ArgumentNullException("There is no rolls to validate.");
            DiceOne = Rolls[0];
            DiceTwo = Rolls.Count == 2 ? Rolls[1] : (int?)null;

            return true;
        }

        #endregion
    }
}