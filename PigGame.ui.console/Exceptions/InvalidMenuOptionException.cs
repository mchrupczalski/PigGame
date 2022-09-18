using System;

namespace PigGame.ui.console.Exceptions
{
    public class InvalidMenuOptionException : Exception
    {
        #region Constructors

        public InvalidMenuOptionException()
        {
        }

        public InvalidMenuOptionException(string message) : base(message)
        {
        }

        #endregion
    }
}