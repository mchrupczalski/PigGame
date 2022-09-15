using PigGame.lib.Models;
using PigGame.lib.Validators;
using Xunit;

namespace PigGame.lib.tests.Models
{
    public class DiceRollModelTests
    {
        private IDiceRollValidator validator = new SingleDiceRollValidator();
        
        
        [Fact]
        public void DiceRollModelSumIsZeroIfOneThrown()
        {
            var diceRoll = new DiceRollModel(validator);
            diceRoll.AddDiceRoll(1);
            Assert.Equal(0, diceRoll.RollScore());
        }

        [Theory]
        [InlineData(2, 0, 2)]
        [InlineData(2, 2, 4)]
        [InlineData(5, 6, 11)]
        [InlineData(6, 0, 6)]
        public void DiceRollModel_SumsRollScore(int rollOne, int rollTwo, int sum)
        {
            var diceRoll = new DiceRollModel(validator);
            diceRoll.AddDiceRoll(rollOne);
            diceRoll.AddDiceRoll(rollTwo);
            
            Assert.Equal(sum, diceRoll.RollScore());
        }
    }
}