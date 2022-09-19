using System;

namespace PigGame.lib.Exceptions
{
    public class OneRolledNoScoreTurnEndException : Exception
    {
        #region Constructors

        public OneRolledNoScoreTurnEndException()
        {
        }

        public OneRolledNoScoreTurnEndException(string message) : base(message)
        {
        }

        #endregion
    }
}