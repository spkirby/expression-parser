using System;

namespace ExpressionParser.Tokens
{
    class BinaryOperatorToken : OperatorToken
    {
        private readonly Func<decimal, decimal, decimal> _func;

        public decimal Execute(decimal leftValue, decimal rightValue)
        {
            return _func(leftValue, rightValue);
        }

        public BinaryOperatorToken(int precedence, string displayString, Func<decimal, decimal, decimal> func)
            : base(precedence, displayString)
        {
            _func = func;
        }
    }
}
