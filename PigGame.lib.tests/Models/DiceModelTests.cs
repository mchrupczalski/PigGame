using System;
using PigGame.lib.Enums;
using PigGame.lib.Models;
using Xunit;

namespace PigGame.lib.tests.Models
{
    public class DiceModelTests
    {
        [Theory]
        [InlineData(DiceType.D4, 4)]
        [InlineData(DiceType.D6, 6)]
        [InlineData(DiceType.D8, 8)]
        [InlineData(DiceType.D10, 10)]
        [InlineData(DiceType.D12, 12)]
        [InlineData(DiceType.D20, 20)]
        public void Roll_DifferentDiceTypes_RollsInRange(DiceType diceType, int diceMax)
        {
            var dice = DiceModelDefaultConstructor(diceType);
            var roll = dice.Roll();
            
            Assert.True(roll >= 1 && roll <= diceMax);
        }
        
        private DiceModel DiceModelDefaultConstructor(DiceType diceType = DiceType.D6) => new DiceModel(diceType);
    }
}