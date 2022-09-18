using System.Collections.Generic;
using PigGame.lib.Exceptions;

namespace PigGame.lib.Validators
{
    public class BigPigRollValidator : TwoDiceRollValidator
    {
        #region Constructors

        /// <inheritdoc />
        public BigPigRollValidator(bool useRuleForDoubles = false) : base(useRuleForDoubles)
        {
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override bool RollIsValid(IEnumerable<int> rolls)
        {
            try
            {
                base.RollIsValid(rolls);
            }
            catch (DoubleOnesRolledScoreLostTurnEndException e)
            {
                throw new DoubleOnesRolledAddsBonusException("You rolled double 1's which adds a bonus!");
            }
            catch (DoubleRolledMustRollCantHoldException e)
            {
                throw new DoubleRolledDoubleTheRollBonusException("You rolled doubles! Your score doubles for this roll!");
            }

            return true;
        }

        #endregion
    }
}