using System.Collections.Generic;
using PigGame.lib.Exceptions;
using PigGame.lib.Validators;
using Xunit;

namespace PigGame.lib.tests.Validators
{
    public class SingleDiceRollValidatorTests
    {
        [Fact]
        public void RollIsValid_OnePassed_ThrowsException()
        {
            var validator = SingleDiceRollValidatorDefaultConstructor();
            var rolls = new List<int>() { 1 };
            Assert.Throws<OneRolledNoScoreTurnEndException>(() => validator.RollIsValid(rolls));
        }

        [Fact]
        public void RollIsValid_HigherThanOnePassed_ReturnsTrue()
        {
            var validator = SingleDiceRollValidatorDefaultConstructor();
            var rolls = new List<int>() { 2 };
            Assert.True(validator.RollIsValid(rolls));
        }
        
        private static SingleDiceRollValidator SingleDiceRollValidatorDefaultConstructor() => new SingleDiceRollValidator();
    }
}