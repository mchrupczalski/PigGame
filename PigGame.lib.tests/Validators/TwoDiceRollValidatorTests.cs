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
        public void RollIsValid_NoDoublesAtLeastOneOnePassed_ThrowsOneRolledNoScoreTurnEndException(int rollOne, int rollTwo)
        {
            var validator = TwoDiceRollValidatorDefaultConstructor(false);
            var rolls = new List<int> { rollOne, rollTwo };
            Assert.Throws<OneRolledNoScoreTurnEndException>(() => validator.RollIsValid(rolls));
        }

        [Fact]
        public void RollIsValid_NoDoublesThreeValuesPassed_ThrowsArgumentOutOfRangeException()
        {
            var validator = TwoDiceRollValidatorDefaultConstructor(false);
            var rolls = new List<int> { 4, 2, 3 };
            Assert.Throws<ArgumentOutOfRangeException>(() => validator.RollIsValid(rolls));
        }

        [Fact]
        public void RollIsValid_NoDoublesOneValuePassed_ThrowsArgumentOutOfRangeException()
        {
            var validator = TwoDiceRollValidatorDefaultConstructor(false);
            var rolls = new List<int> { 4 };
            Assert.Throws<ArgumentOutOfRangeException>(() => validator.RollIsValid(rolls));
        }

        [Fact]
        public void RollIsValid_NoDoublesTwoOnesPassed_ThrowsDoubleOnesRolledScoreLostTurnEndException()
        {
            var validator = TwoDiceRollValidatorDefaultConstructor(false);
            var rolls = new List<int> { 1, 1 };
            Assert.Throws<DoubleOnesRolledScoreLostTurnEndException>(() => validator.RollIsValid(rolls));
        }
        
        [Theory]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 4)]
        public void RollIsValid_WithDoublesTwoSameValuesPassed_ThrowsDoubleRolledMustRollCantHoldException(int rollOne, int rollTwo)
        {
            var validator = TwoDiceRollValidatorDefaultConstructor(true);
            var rolls = new List<int> { rollOne, rollTwo };
            Assert.Throws<DoubleRolledMustRollCantHoldException>(() => validator.RollIsValid(rolls));
        }

        private static TwoDiceRollValidator TwoDiceRollValidatorDefaultConstructor(bool doubles) => new TwoDiceRollValidator(doubles);
    }
}