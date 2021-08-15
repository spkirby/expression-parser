using System;

namespace ExpressionParser
{
    public class InvalidExpressionException : Exception
    {
        private const string DefaultMessage = "The expression was invalid";

        public InvalidExpressionException()
            : base(DefaultMessage)
        {
        }

        public InvalidExpressionException(string message)
            : base(DefaultMessage + ": " + message)
        {
        }

        public InvalidExpressionException(Exception innerException)
            : base(DefaultMessage, innerException)
        {
        }
    }
}
