using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressionParser.Tokens
{
    class TokenCollection
    {
        private readonly IList<Token> infixTokens;
        private readonly IList<Token> postfixTokens;

        public TokenCollection(IList<Token> infixTokens)
        {
            this.infixTokens = infixTokens ?? throw new ArgumentNullException(nameof(infixTokens));
            postfixTokens = ConvertInfixToPostfix(infixTokens);
        }

        public IEnumerable<Token> AsInfix()
        {
            return infixTokens;
        }

        public IEnumerable<Token> AsPostfix()
        {
            return postfixTokens;
        }

        /// <summary>
        /// Reorders Tokens in infix notation into postfix (Reverse Polish) notation.
        /// This method uses the Shunting Yard Algorithm to perform the conversion.
        /// </summary>
        /// <param name="tokens">A list of Tokens in infix order.</param>
        /// <returns>A list of Tokens in postfix order.</returns>
        private IList<Token> ConvertInfixToPostfix(IList<Token> tokens)
        {
            Queue<Token> tokenQueue = new Queue<Token>(tokens);
            Stack<Token> tokenStack = new Stack<Token>();
            IList<Token> output = new List<Token>();

            while (tokenQueue.Any())
            {
                Token token = tokenQueue.Dequeue();

                if (token is ValueToken)
                {
                    output.Add(token);
                }
                else if (token == Token.LeftParenthesis)
                {
                    tokenStack.Push(token);
                }
                else if (token == Token.RightParenthesis)
                {
                    Token popped;

                    // Pop stacked tokens to output until we hit the matching left parenthesis
                    while (tokenStack.Any() && (popped = tokenStack.Pop()) != Token.LeftParenthesis)
                    {
                        output.Add(popped);
                    }
                }
                else
                {
                    // Pop operators from the stack to the output if they have a higher
                    // precedence than the current token
                    while (
                        tokenStack.Any() &&
                        tokenStack.Peek() is OperatorToken &&
                        token.Precedence <= tokenStack.Peek().Precedence)
                    {
                        output.Add(tokenStack.Pop());
                    }

                    tokenStack.Push(token);
                }
            }

            // Pop the remaining stack to output
            while (tokenStack.Any())
            {
                output.Add(tokenStack.Pop());
            }

            return output;
        }
    }
}
