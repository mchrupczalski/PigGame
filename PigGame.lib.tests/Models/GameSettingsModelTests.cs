using System;
using PigGame.lib.Enums;
using PigGame.lib.Models;
using PigGame.lib.Validators;
using Xunit;

namespace PigGame.lib.tests.Models
{
    public class GameSettingsModelTests
    {
        [Theory]
        [InlineData(GameMode.Normal)]
        [InlineData(GameMode.TwoDice)]
        [InlineData(GameMode.BigPig)]
        public void ChangeGameMode_NewGameMode_ChangesTheGameMode(GameMode gameMode)
        {
            var settings = GameSettingsModelDefaultConstructor(gameMode);
            settings.ChangeGameMode(gameMode);

            Assert.Equal(gameMode, settings.GameMode);
        }

        [Theory]
        [InlineData(GameMode.Normal, typeof(SingleDiceRollValidator))]
        [InlineData(GameMode.TwoDice, typeof(TwoDiceRollValidator))]
        [InlineData(GameMode.BigPig, typeof(BigPigRollValidator))]
        public void ChangeGameMode_NewGameMode_ChangesTheValidator(GameMode gameMode, Type validatorType)
        {
            var settings = GameSettingsModelDefaultConstructor(gameMode);
            settings.ChangeGameMode(gameMode);

            Assert.Equal(validatorType, settings.DiceRollValidator.GetType());
        }

        [Fact]
        public void ChangeGameMode_BigPig_ChangesTheDiceCount()
        {
            var settings = GameSettingsModelDefaultConstructor();
            settings.ChangeGameMode(GameMode.BigPig);

            Assert.Equal(2, settings.DiceCount);
        }

        [Fact]
        public void ChangeDiceType_D12_ChangesTheWinningScore()
        {
            var settings = GameSettingsModelDefaultConstructor();
            settings.ChangeDiceType(DiceType.D12);

            Assert.Equal(200, settings.WinScore);
        }

        [Theory]
        [InlineData(DiceType.D4)]
        [InlineData(DiceType.D6)]
        [InlineData(DiceType.D8)]
        [InlineData(DiceType.D10)]
        [InlineData(DiceType.D12)]
        [InlineData(DiceType.D20)]
        public void ChangeDiceType_AllDiceTypes_ChangesTheDiceType(DiceType diceType)
        {
            var settings = GameSettingsModelDefaultConstructor();
            settings.ChangeDiceType(diceType);

            Assert.Equal(diceType, settings.Dice.DiceType);
        }

        private GameSettingsModel GameSettingsModelDefaultConstructor(GameMode gameMode = GameMode.Normal)
        {
            var validatorResolver = new GameSettingsModel.DiceRollValidatorResolver(g =>
            {
                switch (g)
                {
                    case GameMode.Normal:
                        return new SingleDiceRollValidator();
                    case GameMode.TwoDice:
                        return new TwoDiceRollValidator();
                    case GameMode.TwoDiceWithDoubles:
                        return new TwoDiceRollValidator(true);
                    case GameMode.BigPig:
                        return new BigPigRollValidator();
                    case GameMode.BigPigWithDoubles:
                        return new BigPigRollValidator(true);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(g), g, null);
                }
            });
            var dice = new DiceModel();

            return new GameSettingsModel(dice, validatorResolver);
        }
    }
}