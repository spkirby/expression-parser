namespace ExpressionParser.Tokens
{
    abstract class OperatorToken : Token
    {
        public OperatorToken(int precedence)
            : base(precedence)
        {
        }
    }
}
