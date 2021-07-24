using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser
{
    /// <summary>
    /// Represents a Token in an expression. A Token can represent an operator or a value.
    /// </summary>
    class Token
    {
        private static int[] _precedence =
        {
            // Non-operators
            0, 0, 0, 0,
            // Add, subtract
            1, 1,
            // Multiply, divide
            2, 2
        };

        /// <summary>
        /// The type of the Token.
        /// </summary>
        public TokenType Type { get; }

        /// <summary>
        /// The value of the Token, if it is of type TokenType.Value.
        /// </summary>
        public decimal Value { get; }

        /// <summary>
        /// True if the Token is a type of operator (not a value and not a parenthesis).
        /// </summary>
        public bool IsOperator
            => Type >= TokenType.Add && Type <= TokenType.Divide;

        /// <summary>
        /// True if the Token is a type of parenthesis.
        /// </summary>
        public bool IsParenthesis
            => Type == TokenType.LeftParenthesis || Type == TokenType.RightParenthesis;

        /// <summary>
        /// The precedence of a Token that represents an operator. Non-operators have a
        /// precedence of zero.
        /// </summary>
        public int Precedence
            => _precedence[(int)Type];

        public Token(TokenType tokenType)
        {
            Type = tokenType;
            Value = default;
        }

        public Token(decimal value)
        {
            Type = TokenType.Value;
            Value = value;
        }
    }
}
