using System;
using System.Collections.Generic;
using PigGame.lib.Exceptions;

namespace PigGame.lib.Validators
{
    public interface IDiceRollValidator
    {
        /// <summary>
        ///     Validates the result of dice rolls against the game mode rules
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if there is no rolls to validate</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if not enough rolls for the game mode has been added</exception>
        /// <exception cref="NoScoreTurnEndException">
        ///     Thrown if roll is on the reset rule, no score for turn, turn ends for the
        ///     player
        /// </exception>
        bool RollIsValid(IEnumerable<int> rolls);
    }
}