using System.Runtime.InteropServices;
using PigGame.lib.Enums;
using PigGame.lib.Models;
using PigGame.lib.Validators;
using Xunit;

namespace PigGame.lib.tests.Models
{
    public class GameSettingsModelTests
    {
        [Fact]
        public void ChangeGameMode_BigPig_ChangesTheGameMode()
        {
            var settings = GameSettingsModelDefaultConstructor();
            settings.ChangeGameMode(GameMode.BigPig);

            Assert.Equal(GameMode.BigPig, settings.GameMode);
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

        private GameSettingsModel GameSettingsModelDefaultConstructor()
        {
            var validatorResolver = new GameSettingsModel.DiceRollValidatorResolver(g => new SingleDiceRollValidator());
            var dice = new DiceModel();

            return new GameSettingsModel(dice, validatorResolver);
        }
    }
}