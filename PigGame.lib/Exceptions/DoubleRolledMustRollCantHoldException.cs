using System;

namespace PigGame.lib.Exceptions
{
    public class DoubleRolledMustRollCantHoldException : Exception
    {
        #region Constructors

        public DoubleRolledMustRollCantHoldException()
        {
        }

        public DoubleRolledMustRollCantHoldException(string message) : base(message)
        {
        }

        #endregion
    }
}