using PigGame.lib.Enums;
using PigGame.lib.Factories;
using PigGame.lib.Validators;

namespace PigGame.lib.Models
{
    public class GameSettingsModel
    {
        #region Fields

        private readonly DIceRollValidatorFactory _validatorFactory;

        #endregion

        #region Properties

        public int DiceCount { get; private set; } 

        public DiceModel Dice { get; }
        public GameMode GameMode { get; private set; }
        public int WinScore { get; private set; } = 100;
        public IDiceRollValidator DiceRollValidator { get; private set; }

        #endregion

        #region Constructors

        public GameSettingsModel(DiceModel dice, DIceRollValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
            Dice = dice;
            ChangeGameMode(GameMode.Normal);
        }

        #endregion

        public void ChangeGameMode(GameMode mode)
        {
            DiceCount = mode == GameMode.Normal ? 1 : 2;
            GameMode = mode;
            DiceRollValidator = _validatorFactory.Create(mode);
        }

        public void SetWinningScore(int winScore) => WinScore = winScore;
    }
}