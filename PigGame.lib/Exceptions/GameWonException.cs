using System;

namespace PigGame.lib.Exceptions
{
    public class GameWonException : Exception
    {
        #region Constructors

        public GameWonException()
        {
        }

        public GameWonException(string message) : base(message)
        {
        }

        #endregion
    }
}