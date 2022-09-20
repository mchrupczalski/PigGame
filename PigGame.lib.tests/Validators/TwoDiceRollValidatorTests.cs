using System;
using System.Collections.Generic;
using PigGame.lib.Exceptions;
using PigGame.lib.Validators;
using Xunit;

namespace PigGame.lib.tests.Validators
{
    public class TwoDiceRollValidatorTests
    {
        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        public void RollIsValid_NoDoublesAtLeastOneOnePassed_ThrowsException(int rollOne, int rollTwo)
        {
            var validator = TwoDiceRollValidatorDefaultConstructor(false);
            var rolls = new List<int>() { rollOne, rollTwo };
            Assert.Throws<OneRolledNoScoreTurnEndException>(() => validator.RollIsValid(rolls));
        }
        
        [Fact]
        public void RollIsValid_NoDoublesThreeValuesPassed_ThrowsException()
        {
            var validator = TwoDiceRollValidatorDefaultConstructor(false);
            var rolls = new List<int>() { 4, 2, 3 };
            Assert.Throws<ArgumentOutOfRangeException>(() => validator.RollIsValid(rolls));
        }
        
        [Fact]
        public void RollIsValid_NoDoublesTwoOnesPassed_ThrowsException()
        {
            var validator = TwoDiceRollValidatorDefaultConstructor(true);
            var rolls = new List<int>() { 1, 1 };
            Assert.Throws<DoubleOnesRolledScoreLostTurnEndException>(() => validator.RollIsValid(rolls));
        }
        
        private static TwoDiceRollValidator TwoDiceRollValidatorDefaultConstructor(bool doubles) => new TwoDiceRollValidator(doubles);
    }
}