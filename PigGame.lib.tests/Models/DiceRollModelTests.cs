using System.Collections.Generic;
using PigGame.lib.Models;
using Xunit;

namespace PigGame.lib.tests.Models
{
    public class DiceRollModelTests
    {
        [Fact]
        public void AddDiceRolls_AddingRolls_UpdatesTheListOfLatestRolls()
        {
            var rollModel = DiceRollModelDefaultConstructor();
            var rolls = new List<int>() { 2, 3 };
            
            rollModel.AddDicesRoll(rolls);
            Assert.Equal(rolls, rollModel.LatestRolls);
        }

        [Fact]
        public void RollsScore_ScoreOverridenWithZero_ReturnsZero()
        {
            var rollModel = DiceRollModelDefaultConstructor();
            var rolls = new List<int>() { 2, 3 };
            rollModel.AddDicesRoll(rolls);
            
            rollModel.OverrideScore(0);
            Assert.Equal(0, rollModel.RollsScore());
        }

        [Fact]
        public void RollScore_AddingRolls_ReturnsCorrectSum()
        {
            var rollModel = DiceRollModelDefaultConstructor();
            var rolls = new List<int>() { 2, 3 };
            rollModel.AddDicesRoll(rolls);
            
            Assert.Equal(5, rollModel.RollsScore());
        }
        
        [Fact]
        public void RollScore_AddingRollsAndBonus_ReturnsCorrectSumWithBonus()
        {
            var rollModel = DiceRollModelDefaultConstructor();
            var rolls = new List<int>() { 2, 3 };
            rollModel.AddDicesRoll(rolls);
            rollModel.AddRollBonus(25);
            
            Assert.Equal(30, rollModel.RollsScore());
        }

        private DiceRollModel DiceRollModelDefaultConstructor() => new DiceRollModel();
    }
}