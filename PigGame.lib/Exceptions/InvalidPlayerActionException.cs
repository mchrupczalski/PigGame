using System;

namespace PigGame.lib.Exceptions
{
    public class InvalidPlayerActionException : Exception
    {
        #region Constructors

        public InvalidPlayerActionException()
        {
        }

        public InvalidPlayerActionException(string message) : base(message)
        {
        }

        #endregion
    }
}