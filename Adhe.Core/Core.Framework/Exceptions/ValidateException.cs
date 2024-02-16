using System;

namespace Core.Framework
{
    public class ValidateException : Exception
    {
        public ValidateException(string Message) : base(Message)
        { }
    }
}
