using System;

namespace FisherConfig.Exceptions
{
    public class ConfigNotInitializedException : Exception
    {
        public ConfigNotInitializedException()
        {
        }

        public ConfigNotInitializedException(string message)
            : base(message)
        {
        }

        public ConfigNotInitializedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}