using System;

namespace PigGame.ui.console.Exceptions
{
    public class GameTerminatedException : Exception
    {
        #region Constructors

        public GameTerminatedException()
        {
        }

        public GameTerminatedException(string message) : base(message)
        {
        }

        #endregion
    }
}