using System;
using System.Collections.Generic;
using System.Linq;
using PigGame.lib.Enums;

namespace PigGame.lib.Validators
{
    public abstract class DiceRollValidatorBase : IDiceRollValidator
    {
        #region Properties

        protected List<int> Rolls { get; set; }
        protected int DiceOne { get; set; }
        protected int? DiceTwo { get; set; }

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