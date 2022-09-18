using System.Collections.Generic;
using System.Linq;
using PigGame.lib.Enums;
using PigGame.lib.Validators;

namespace PigGame.lib.Models
{
    public class GameSettingsModel
    {
        #region Delegates

        public delegate IDiceRollValidator DiceRollValidatorResolver(GameMode gameModeKey);

        #endregion

        #region Fields

        public IEnumerable<(DiceType diceType, int winScore)> DiceTypeWinScores { get; } = new List<(DiceType diceType, int winScore)>
        {
            (DiceType.D4, 67),
            (DiceType.D6, 100),
            (DiceType.D8, 133),
            (DiceType.D10, 167),
            (DiceType.D12, 200),
            (DiceType.D20, 333)
        };

        private readonly DiceRollValidatorResolver _validatorResolver;

        #endregion

        #region Properties

        public int DiceCount { get; private set; }

        public DiceModel Dice { get; }
        public GameMode GameMode { get; private set; }
        public int WinScore { get; private set; } = 100;
        public IDiceRollValidator DiceRollValidator { get; private set; }

        #endregion

        #region Constructors

        public GameSettingsModel(DiceModel dice, DiceRollValidatorResolver validatorResolver)
        {
            _validatorResolver = validatorResolver;
            Dice = dice;
            ChangeGameMode(GameMode.Normal);
        }

        #endregion

        public void ChangeGameMode(GameMode mode)
        {
            DiceCount = mode == GameMode.Normal ? 1 : 2;
            GameMode = mode;
            DiceRollValidator = _validatorResolver(mode);
        }

        public void ChangeDiceType(DiceType diceType)
        {
            Dice.SetDiceType(diceType);
            SetWinningScore(DiceTypeWinScores.First(d => d.diceType == diceType).winScore);
        }

        public void SetWinningScore(int winScore) => WinScore = winScore;
    }
}