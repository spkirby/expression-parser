namespace ExpressionParser.Tokens
{
    abstract class OperatorToken : Token
    {
        public OperatorToken(int precedence, string displayString)
            : base(precedence, displayString)
        {
        }
    }
}
