using System;

namespace PigGame.lib.Exceptions
{
    public class NoScoreTurnEndException : Exception
    {
        public NoScoreTurnEndException()
        {
        }

        public NoScoreTurnEndException(string message) :base(message)
        {
        }
    }
}