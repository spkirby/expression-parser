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

        public UnaryOperatorToken(int precedence, Func<decimal, decimal> func)
            : base(precedence)
        {
            _func = func;
        }
    }
}
