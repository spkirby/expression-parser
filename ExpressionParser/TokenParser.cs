using ExpressionParser.Tokens;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ExpressionParser
{
    /// <summary>
    /// Parses string expressions into Tokens.
    /// </summary>
    class TokenParser
    {
        private readonly Regex numericRegex = new Regex(@"-?[0-9]+\.?[0-9]*");
        private int index;
        private string expression;
        private Token previousToken;

        /// <summary>
        /// Parses an expression from a string into a list of Tokens.
        /// </summary>
        /// <param name="expression">An expression as a string.</param>
        /// <returns>The expression as a list of Tokens.</returns>
        public TokenCollection ParseExpression(string expression)
        {
            index = 0;
            this.expression = expression;
            previousToken = null;

            var tokens = new List<Token>();
            Token token = GetNextToken();

            while (token != null)
            {
                if (token == Token.Invalid)
                {
                    throw new InvalidTokenException();
                }

                previousToken = token;
                tokens.Add(token);

                token = GetNextToken();
            }

            return new TokenCollection(tokens);
        }

        /// <summary>
        /// Returns the next Token from the expression string.
        /// </summary>
        /// <returns>A Token, or null if there are no more tokens to return.</returns>
        protected Token GetNextToken()
        {
            if (!MoveToNextChar())
            {
                return null;
            }

            return IsValueToken()
                ? ReadValueToken()
                : ReadNonValueToken();
        }

        /// <summary>
        /// Returns true if the next token appears to be a value.
        /// </summary>
        protected bool IsValueToken()
        {
            char ch = expression[index];

            if (char.IsDigit(ch))
            {
                return true;
            }
            else
            {
                return ch == '-'
                    && !IsSubtractContext()
                    && (index + 1) < expression.Length
                    && char.IsDigit(expression[index + 1]);
            }
        }

        /// <summary>
        /// Advances the index to the next non-whitespace character.
        /// </summary>
        protected bool MoveToNextChar()
        {
            while (index < expression.Length && char.IsWhiteSpace(expression[index]))
            {
                index++;
            }

            return index < expression.Length;
        }

        /// <summary>
        /// Returns true if the previous token means that "-" should refer to subtraction
        /// rather than negation.
        /// </summary>
        protected bool IsSubtractContext()
        {
            return previousToken == Token.RightParenthesis
                || previousToken is ValueToken;
        }

        protected Token ReadNonValueToken()
        {
            switch (expression[index++])
            {
                case '(':
                    return Token.LeftParenthesis;
                case ')':
                    return Token.RightParenthesis;
                case '+':
                    return Token.Add;
                case '-':
                    return IsSubtractContext()
                        ? (Token)Token.Subtract
                        : (Token)Token.Negate;
                case '*':
                    return Token.Multiply;
                case '/':
                    return Token.Divide;
                default:
                    return Token.Invalid;
            }
        }

        /// <summary>
        /// Parses a number (integer or decimal) in the expression string,
        /// starting at the current index.
        /// </summary>
        /// <returns>A Token representing the parsed value.</returns>
        protected Token ReadValueToken()
        {
            Match match = numericRegex.Match(expression, index);
            Token token;

            if (match.Success && decimal.TryParse(match.Value, out decimal value))
            {
                token = new ValueToken(value);
                index += match.Length;
            }
            else
            {
                token = Token.Invalid;
            }

            return token;
        }
    }
}
