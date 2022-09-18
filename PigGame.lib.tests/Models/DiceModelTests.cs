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
        public void Dice_RollsInRange(DiceType diceType, int diceMax)
        {
            var d = new DiceModel(diceType);
            var roll = d.Roll();
            
            Assert.True(roll >= 1 && roll <= diceMax);
        }
    }
}