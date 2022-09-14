using System;
using PigGame.lib.Models;
using Xunit;

namespace PigGame.lib.tests.Models
{
    public class DiceModelTests
    {
        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        public void Dice_CreatedSuccessfully(int diceMax) => new DiceModel(diceMax);
        

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Dice_CreationThrowsError(int diceMax) => Assert.Throws<ArgumentOutOfRangeException>(() => _ = new DiceModel(diceMax));

        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        public void Dice_RollsInRange(int diceMax)
        {
            var d = new DiceModel(diceMax);
            var roll = d.Roll();
            
            Assert.True(roll >= 1 && roll <= diceMax);
        }
    }
}