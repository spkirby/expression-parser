using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpressionParser
{
    /// <summary>
    /// Parses string expressions into Tokens.
    /// </summary>
    class TokenParser
    {
        /// <summary>
        /// Result from the ReadNumber method.
        /// </summary>
        protected struct ReadNumberResult
        {
            public decimal Value { get; set; }
            public int NextIndex { get; set; }
        }

        private int index;
        private string expression;

        /// <summary>
        /// Parses an expression from a string into a list of Tokens.
        /// </summary>
        /// <param name="expression">An expression as a string.</param>
        /// <returns>The expression as a list of Tokens.</returns>
        public IList<Token> ParseExpression(string expression)
        {
            this.index = 0;
            this.expression = expression;

            IList<Token> tokens = new List<Token>();
            Token? token;

            while ((token = GetNextToken()).HasValue)
            {
                if (token.Value.Type == TokenType.Invalid)
                {
                    throw new InvalidTokenException();
                }
                else
                {
                    tokens.Add(token.Value);
                }
            }

            return tokens;
        }

        /// <summary>
        /// Returns the next Token from the expression string.
        /// </summary>
        /// <returns>A Token, or null if there are no more tokens to return.</returns>
        protected Token? GetNextToken()
        {
            // Reached the end of the expression, so stop
            if(index >= expression.Length)
            {
                return null;
            }

            // Skip over any whitespace
            while (Char.IsWhiteSpace(expression[index]))
            {
                index++;
            }

            Token token = new Token();
            char ch = expression[index];

            if(Char.IsDigit(ch))
            {
                ReadNumberResult result = ReadNumber(expression, index);
                index = result.NextIndex;

                token.Type = TokenType.Value;
                token.Value = result.Value;
            }
            else
            {
                switch(ch)
                {
                    case '(': token.Type = TokenType.LeftParenthesis; break;
                    case ')': token.Type = TokenType.RightParenthesis; break;
                    case '+': token.Type = TokenType.Add; break;
                    case '-': token.Type = TokenType.Subtract; break;
                    case '*': token.Type = TokenType.Multiply; break;
                    case '/': token.Type = TokenType.Divide; break;
                    default : token.Type = TokenType.Invalid; break;
                }

                index++;
            }

            return token;
        }

        /// <summary>
        /// Parses a number (integer or decimal) in a string, starting at the given index.
        /// </summary>
        /// <param name="str">The string to parse.</param>
        /// <param name="startIndex">The index at which to start parsing the string.</param>
        /// <returns>A ReadNumberResult with the parsed number and index of the first
        /// character after the parsed number.</returns>
        protected ReadNumberResult ReadNumber(string str, int startIndex)
        {
            Regex regex = new Regex(@"[0-9]+\.?[0-9]*");
            Match match = regex.Match(str.Substring(startIndex));

            return new ReadNumberResult()
            {
                NextIndex = startIndex + match.Length,
                Value = decimal.Parse(match.Value)
            };
        }
    }
}
