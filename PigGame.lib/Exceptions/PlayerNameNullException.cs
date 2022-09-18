using System;

namespace PigGame.lib.Exceptions
{
    public class PlayerNameNullException : Exception
    {
        #region Constructors

        public PlayerNameNullException()
        {
        }

        public PlayerNameNullException(string message) : base(message)
        {
        }

        #endregion
    }
}