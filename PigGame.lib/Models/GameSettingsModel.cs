using System.Collections.Generic;
using System.Linq;
using PigGame.lib.Enums;
using PigGame.lib.Validators;

namespace PigGame.lib.Models
{
    public class GameSettingsModel
    {
        #region Delegates

        /// <summary>
        ///     Factory method for creating Dice Roll Validators
        /// </summary>
        public delegate IDiceRollValidator DiceRollValidatorResolver(GameMode gameModeKey);

        #endregion

        #region Fields

        private readonly DiceRollValidatorResolver _validatorResolver;

        #endregion

        #region Properties

        /// <summary>
        ///     Holds default Winning Score values for given Dice Type
        /// </summary>
        public IEnumerable<(DiceType diceType, int winScore)> DiceTypeWinScores { get; } = new List<(DiceType diceType, int winScore)>
        {
            (DiceType.D4, 67),
            (DiceType.D6, 100),
            (DiceType.D8, 133),
            (DiceType.D10, 167),
            (DiceType.D12, 200),
            (DiceType.D20, 333)
        };

        /// <summary>
        ///     A number of dices used in the game
        /// </summary>
        public int DiceCount { get; private set; }

        /// <summary>
        ///     The Dice object used for the game
        /// </summary>
        public DiceModel Dice { get; }

        /// <summary>
        ///     The current <see cref="GameMode" /> for the game
        /// </summary>
        public GameMode GameMode { get; private set; }

        /// <summary>
        ///     An upper limit for scores that player must reach to win the game
        /// </summary>
        public int WinScore { get; private set; } = 100;

        /// <summary>
        ///     The Dice Roll Validator, which validates dice rolls results
        /// </summary>
        public IDiceRollValidator DiceRollValidator { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Creates a new instance of the <see cref="GameSettingsModel" />
        /// </summary>
        /// <param name="dice">The dice object</param>
        /// <param name="validatorResolver">A factory method to create Dice Roll Validators</param>
        public GameSettingsModel(DiceModel dice, DiceRollValidatorResolver validatorResolver)
        {
            _validatorResolver = validatorResolver;
            Dice = dice;
            ChangeGameMode(GameMode.Normal);
        }

        #endregion

        /// <summary>
        ///     Changes the Game Mode
        /// </summary>
        /// <param name="mode">The new game mode for the game</param>
        public void ChangeGameMode(GameMode mode)
        {
            DiceCount = mode == GameMode.Normal ? 1 : 2;
            GameMode = mode;
            DiceRollValidator = _validatorResolver(mode);
        }

        /// <summary>
        ///     Changes the dice type
        /// </summary>
        /// <param name="diceType">The new dice type</param>
        public void ChangeDiceType(DiceType diceType)
        {
            Dice.SetDiceType(diceType);
            SetWinningScore(DiceTypeWinScores.First(d => d.diceType == diceType)
                                             .winScore);
        }

        /// <summary>
        ///     Sets the Winning Score for the game
        /// </summary>
        /// <param name="winScore">The new win score</param>
        public void SetWinningScore(int winScore) => WinScore = winScore;
    }
}