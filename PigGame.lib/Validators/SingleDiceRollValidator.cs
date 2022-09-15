using System;
using System.Collections.Generic;
using System.Linq;
using PigGame.lib.Exceptions;

namespace PigGame.lib.Validators
{
    public class SingleDiceRollValidator : IDiceRollValidator
    {
        #region Static Fields and Const

        private const int _maxDices = 1;

        #endregion
        
        /// <inheritdoc />
        public bool RollIsValid(IEnumerable<int> rolls)
        {
            var rollsValidationList = rolls.ToList();
            
            if (rollsValidationList.Count == 0) throw new ArgumentNullException("There is no rolls to validate.");
            if (rollsValidationList.Count < _maxDices) throw new ArgumentOutOfRangeException($"There is no enough rolls to validate. Current: ({rollsValidationList.Count}) : Expected: ({_maxDices})");

            var diceOneThrow = rollsValidationList[0];
            if (diceOneThrow == 1) throw new NoScoreTurnEndException("You thrown 1, your turn ended with no score.");
            
            return true;
        }
    }
}