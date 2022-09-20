using System;
using System.Collections.Generic;
using PigGame.lib.Exceptions;

namespace PigGame.lib.Validators
{
    public interface IDiceRollValidator
    {
        #region Abstract Members

        /// <summary>
        ///     Validates the result of dice rolls against the game mode rules
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     All Game Modes: Thrown if there is no rolls to validate
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     All Game Modes: Thrown if not enough rolls for the game mode has been added
        /// </exception>
        /// <exception cref="OneRolledNoScoreTurnEndException">
        ///     All Game Modes: Thrown if at least 1 is rolled, no score for turn, turn ends for the
        ///     player.
        /// </exception>
        /// <exception cref="DoubleOnesRolledAddsBonusException">
        ///     Thrown if player rolled two ones and double rule is active for the game mode.
        /// </exception>
        /// <exception cref="DoubleRolledDoubleTheRollBonusException">
        ///     TwoDice and BigPig Mode: Thrown if player rolled double (both values are the same)
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="DoubleOnesRolledScoreLostTurnEndException"></exception>
        /// <exception cref="DoubleRolledMustRollCantHoldException"></exception>
        bool RollIsValid(IEnumerable<int> rolls);

        #endregion
    }
}

/// <inheritdoc />