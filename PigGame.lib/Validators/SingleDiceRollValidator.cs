using System.Collections.Generic;
using PigGame.lib.Exceptions;

namespace PigGame.lib.Validators
{
    public class SingleDiceRollValidator : DiceRollValidatorBase
    {
        /// <inheritdoc />
        public override bool RollIsValid(IEnumerable<int> rolls)
        {
            base.RollIsValid(rolls);
            if (DiceOne == 1) throw new OneRolledNoScoreTurnEndException("You thrown 1, your turn ended with no score.");

            return true;
        }
    }
}