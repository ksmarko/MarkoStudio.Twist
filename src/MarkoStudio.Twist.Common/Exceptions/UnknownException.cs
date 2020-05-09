using System;

namespace MarkoStudio.Twist.Common.Exceptions
{
    public class UnknownException : Exception
    {
        public UnknownException(string message) : base(message) { }
    }
}
