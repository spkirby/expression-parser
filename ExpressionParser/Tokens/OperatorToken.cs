namespace ExpressionParser.Tokens
{
    public abstract class OperatorToken : Token
    {
        public OperatorToken(string symbol, int precedence)
            : base(symbol, precedence)
        {
        }
    }
}
