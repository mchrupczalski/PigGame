using System;

namespace PigGame.lib.Exceptions
{
    public class PlayerNameIsTakenException : Exception
    {
        #region Constructors

        public PlayerNameIsTakenException()
        {
        }

        public PlayerNameIsTakenException(string message) : base(message)
        {
        }

        #endregion
    }
}