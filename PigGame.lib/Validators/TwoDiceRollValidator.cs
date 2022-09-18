using System;
using System.Collections.Generic;
using PigGame.lib.Exceptions;

namespace PigGame.lib.Validators
{
    public class TwoDiceRollValidator : SingleDiceRollValidator
    {
        #region Fields

        private readonly bool _useRuleForDoubles;

        #endregion

        #region Constructors

        public TwoDiceRollValidator(bool useRuleForDoubles = false)
        {
            _useRuleForDoubles = useRuleForDoubles;
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override bool RollIsValid(IEnumerable<int> rolls)
        {
            try
            {
                base.RollIsValid(rolls);
                if (Rolls.Count < 2)
                    throw new ArgumentOutOfRangeException(nameof(rolls), $"There is not enough rolls. Passed {Rolls.Count}, expected 2");

                if (Rolls.Count > 2)
                    throw new ArgumentOutOfRangeException(nameof(rolls), $"There is too many rolls. Passed {Rolls.Count}, expected 2");

                if (DiceTwo == 1) 
                    throw new OneRolledNoScoreTurnEndException("You thrown 1, your turn ended with no score.");
            }
            // two dice rule overrides single 1, if two 1's are rolled
            catch (OneRolledNoScoreTurnEndException)
            {
                if (DiceOne == 1 && DiceTwo == 1)
                    throw new DoubleOnesRolledScoreLostTurnEndException("You rolled two 1's. Your entire score is lost. Turn ended.");

                throw;
            }


            if (_useRuleForDoubles && DiceOne == DiceTwo)
                throw new DoubleRolledMustRollCantHoldException($"You rolled doubles! ({DiceOne}:{DiceTwo}). You must roll again!");

            // base will validate against single 1 value

            return true;
        }

        #endregion
    }
}