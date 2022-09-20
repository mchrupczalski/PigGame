using System.Collections.Generic;
using PigGame.lib.Exceptions;
using PigGame.lib.Validators;
using Xunit;

namespace PigGame.lib.tests.Validators
{
    public class BigPigRollValidatorTests
    {
        [Fact]
        public void RollIsValid_NoDoublesTwoOnesRolled_ThrowsDoubleOnesRolledAddsBonusException()
        {
            var validator = BigPigRollValidatorDefaultConstructor(false);
            var rolls = new List<int> { 1,1 };
            Assert.Throws<DoubleOnesRolledAddsBonusException>(() => validator.RollIsValid(rolls));
        }
        
        [Theory]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 4)]
        public void RollIsValid_NoDoublesTwoOnesRolled_ThrowsDoubleRolledDoubleTheRollBonusException(int rollOne, int rollTwo)
        {
            var validator = BigPigRollValidatorDefaultConstructor(true);
            var rolls = new List<int> { rollOne, rollTwo };
            Assert.Throws<DoubleRolledDoubleTheRollBonusException>(() => validator.RollIsValid(rolls));
        }
        
        private static BigPigRollValidator BigPigRollValidatorDefaultConstructor(bool doubles) => new BigPigRollValidator(doubles);
    }
}