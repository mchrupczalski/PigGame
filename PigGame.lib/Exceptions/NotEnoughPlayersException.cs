using System;

namespace PigGame.lib.Exceptions
{
    public class NotEnoughPlayersException : Exception
    {
        #region Constructors

        public NotEnoughPlayersException()
        {
        }

        public NotEnoughPlayersException(string message) : base(message)
        {
        }

        #endregion
    }
}