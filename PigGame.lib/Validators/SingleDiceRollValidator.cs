using System;
using System.Collections.Generic;
using System.Linq;
using PigGame.lib.Exceptions;

namespace PigGame.lib.Validators
{
    public class SingleDiceRollValidator : IDiceRollValidator
    {
       
        
        /// <inheritdoc />
        public bool RollIsValid(IEnumerable<int> rolls)
        {
            var rollsValidationList = rolls.ToList();
            
            if (rollsValidationList.Count == 0) throw new ArgumentNullException("There is no rolls to validate.");

            var diceOneThrow = rollsValidationList[0];
            if (diceOneThrow == 1) throw new NoScoreTurnEndException("You thrown 1, your turn ended with no score.");
            
            return true;
        }
    }
}