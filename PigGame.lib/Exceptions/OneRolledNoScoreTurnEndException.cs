using System;

namespace PigGame.lib.Exceptions
{
    public class OneRolledNoScoreTurnEndException : Exception
    {
        public OneRolledNoScoreTurnEndException()
        {
        }

        public OneRolledNoScoreTurnEndException(string message) :base(message)
        {
        }
    }
}