namespace ExpressionParser.Tokens
{
    class Token
    {
        public static Token Invalid { get; }
            = new Token(0, "?");

        public static Token LeftParenthesis { get; }
            = new Token(0, "(");

        public static Token RightParenthesis { get; }
            = new Token(0, ")");

        public static BinaryOperatorToken Add { get; }
            = new BinaryOperatorToken(1, "+", (a, b) => a + b);

        public static BinaryOperatorToken Subtract { get; }
            = new BinaryOperatorToken(1, "-", (a, b) => a - b);

        public static BinaryOperatorToken Multiply { get; }
            = new BinaryOperatorToken(2, "*", (a, b) => a * b);

        public static BinaryOperatorToken Divide { get; }
            = new BinaryOperatorToken(2, "/", (a, b) => a / b);

        public static UnaryOperatorToken Negate { get; }
            = new UnaryOperatorToken(3, "-", a => -a);

        public int Precedence { get; }
        public string DisplayString { get; }

        protected Token(int precedence, string displayString)
        {
            Precedence = precedence;
            DisplayString = displayString;
        }

        public override string ToString()
        {
            return DisplayString;
        }
    }
}
