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
        public IList<Token> ParseExpression(string expression)
        {
            index = 0;
            this.expression = expression;
            previousToken = null;

            var tokens = new List<Token>();
            Token token = GetNextToken();

            while (token != null)
            {
                if (token.Type == TokenType.Invalid)
                {
                    throw new InvalidTokenException();
                }

                previousToken = token;
                tokens.Add(token);

                token = GetNextToken();
            }

            return tokens;
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
            return previousToken?.Type == TokenType.RightParenthesis
                || previousToken?.Type == TokenType.Value;
        }

        protected Token ReadNonValueToken()
        {
            switch (expression[index++])
            {
                case '(':
                    return new Token(TokenType.LeftParenthesis);
                case ')':
                    return new Token(TokenType.RightParenthesis);
                case '+':
                    return new Token(TokenType.Add);
                case '-':
                    return IsSubtractContext()
                        ? new Token(TokenType.Subtract)
                        : new Token(TokenType.Negate);
                case '*':
                    return new Token(TokenType.Multiply);
                case '/':
                    return new Token(TokenType.Divide);
                default:
                    return new Token(TokenType.Invalid);
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
                token = new Token(value);
                index += match.Length;
            }
            else
            {
                token = new Token(TokenType.Invalid);
            }

            return token;
        }
    }
}
