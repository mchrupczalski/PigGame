using System;

namespace PigGame.lib.Exceptions
{
    public class DoubleRolledDoubleTheRollBonusException : Exception
    {
        #region Constructors

        public DoubleRolledDoubleTheRollBonusException()
        {
        }

        public DoubleRolledDoubleTheRollBonusException(string message) : base(message)
        {
        }

        #endregion
    }
}