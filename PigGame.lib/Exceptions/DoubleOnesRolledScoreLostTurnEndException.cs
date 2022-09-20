using System;

namespace PigGame.lib.Exceptions
{
    public class DoubleOnesRolledScoreLostTurnEndException : Exception
    {
        #region Constructors

        public DoubleOnesRolledScoreLostTurnEndException()
        {
        }

        public DoubleOnesRolledScoreLostTurnEndException(string message) : base(message)
        {
        }

        #endregion
    }
}