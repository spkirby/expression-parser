using System;

namespace ExpressionParser.Tokens
{
    public class UnaryOperatorToken : OperatorToken
    {
        private readonly Func<decimal, decimal> func;

        public UnaryOperatorToken(string symbol, int precedence, Func<decimal, decimal> func)
            : base(symbol, precedence)
        {
            this.func = func;
        }

        public decimal Evaluate(decimal value)
        {
            return func(value);
        }
    }
}
