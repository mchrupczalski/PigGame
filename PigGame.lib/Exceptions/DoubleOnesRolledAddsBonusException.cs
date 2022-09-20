using System;

namespace PigGame.lib.Exceptions
{
    public class DoubleOnesRolledAddsBonusException : Exception
    {
        #region Constructors

        public DoubleOnesRolledAddsBonusException()
        {
        }

        public DoubleOnesRolledAddsBonusException(string message) : base(message)
        {
        }

        #endregion
    }
}