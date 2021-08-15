using System;

namespace ExpressionParser.Tokens
{
    public class BinaryOperatorToken : OperatorToken
    {
        private readonly Func<decimal, decimal, decimal> func;

        public BinaryOperatorToken(string symbol, int precedence, Func<decimal, decimal, decimal> func)
            : base(symbol, precedence)
        {
            this.func = func;
        }

        public decimal Evaluate(decimal leftValue, decimal rightValue)
        {
            return func(leftValue, rightValue);
        }
    }
}
