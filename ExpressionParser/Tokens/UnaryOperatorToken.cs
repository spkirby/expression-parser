using System;

namespace ExpressionParser.Tokens
{
    class UnaryOperatorToken : OperatorToken
    {
        private readonly Func<decimal, decimal> _func;

        public decimal Execute(decimal value)
        {
            return _func(value);
        }

        public UnaryOperatorToken(int precedence, string displayString, Func<decimal, decimal> func)
            : base(precedence, displayString)
        {
            _func = func;
        }
    }
}
