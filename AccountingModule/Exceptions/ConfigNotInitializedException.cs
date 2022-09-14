using System;

namespace AccountingModule.Exceptions
{
    public class InvalidWalException : Exception
    {
        public InvalidWalException()
        {
        }

        public InvalidWalException(string message)
            : base(message)
        {
        }

        public InvalidWalException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}