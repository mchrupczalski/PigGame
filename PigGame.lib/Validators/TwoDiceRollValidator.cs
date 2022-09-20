using System;
using System.Collections.Generic;
using PigGame.lib.Exceptions;

namespace PigGame.lib.Validators
{
    public class TwoDiceRollValidator : SingleDiceRollValidator
    {
        #region Fields

        protected readonly bool UseRuleForDoubles;

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates a new instance of <see cref="TwoDiceRollValidator" />
        /// </summary>
        /// <param name="useRuleForDoubles">Activate special validation rule for Two Dice game sub-variant</param>
        public TwoDiceRollValidator(bool useRuleForDoubles = false)
        {
            UseRuleForDoubles = useRuleForDoubles;
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

            if (UseRuleForDoubles && DiceOne == DiceTwo)
                throw new DoubleRolledMustRollCantHoldException($"You rolled doubles! ({DiceOne}:{DiceTwo}). You must roll again!");

            return true;
        }

        #endregion
    }
}