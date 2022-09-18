using System;

namespace PigGame.lib.Exceptions
{
    public class MustRollCantHoldException : Exception
    {
        #region Constructors

        public MustRollCantHoldException()
        {
        }

        public MustRollCantHoldException(string message) : base(message)
        {
        }

        #endregion
    }
}