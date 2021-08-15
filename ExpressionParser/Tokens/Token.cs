using System;

namespace ExpressionParser.Tokens
{
    public class Token
    {
        public static Token Invalid
            = new Token("Invalid", 0);

        public static BinaryOperatorToken Add
            = new BinaryOperatorToken("+", 1, (a, b) => a + b);

        public static BinaryOperatorToken Subtract
            = new BinaryOperatorToken("-", 1, (a, b) => a - b);

        public static BinaryOperatorToken Multiply
            = new BinaryOperatorToken("*", 2, (a, b) => a * b);

        public static BinaryOperatorToken Divide
            = new BinaryOperatorToken("/", 2, (a, b) => a / b);

        public static BinaryOperatorToken Power
            = new BinaryOperatorToken("^", 3, (a, b) => (decimal)Math.Pow((double)a, (double)b));

        public static UnaryOperatorToken Negate
            = new UnaryOperatorToken("-", 4, (a) => -a);

        public static Token LeftParenthesis
            = new Token("(", 5);

        public static Token RightParenthesis
            = new Token(")", 5);

        public int Precedence { get; }
        public string Symbol { get; }

        public Token(string symbol, int precedence)
        {
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            Precedence = precedence;
        }

        public override string ToString()
        {
            return Symbol;
        }
    }
}
