using ExpressionParser.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser
{
    /// <summary>
    /// Evaluates mathematical expressions represented by lists of Tokens.
    /// </summary>
    class Evaluator
    {
        /// <summary>
        /// Evaluates a list of Tokens representing a mathematical
        /// expression and returns the result.
        /// </summary>
        /// <param name="tokenList">A list of Tokens.</param>
        /// <returns>The result of the expression.</returns>
        public decimal Evaluate(IList<Token> tokenList)
        {
            IList<Token> infixTokens = ConvertInfixToPostfix(tokenList);
            return EvaluatePostfixTokens(infixTokens);
        }

        /// <summary>
        /// Reorders Tokens in infix notation into postfix (Reverse Polish) notation.
        /// This method uses the Shunting Yard Algorithm to perform the conversion.
        /// </summary>
        /// <param name="tokenList">A list of Tokens in infix order.</param>
        /// <returns>A list of Tokens in postfix order.</returns>
        private IList<Token> ConvertInfixToPostfix(IList<Token> tokenList)
        {
            Queue<Token> tokenQueue = new Queue<Token>(tokenList);

            IList<Token> output = new List<Token>();
            Stack<Token> tokenStack = new Stack<Token>();

            while (tokenQueue.Count > 0)
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
                    while (tokenStack.Count > 0 && (popped = tokenStack.Pop()) != Token.LeftParenthesis)
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
            while (tokenStack.Count > 0)
            {
                output.Add(tokenStack.Pop());
            }

            return output;
        }

        /// <summary>
        /// Evaluates an expression represented by a list of Tokens in postfix (Reverse Polish)
        /// notation and returns the result.
        /// </summary>
        /// <param name="tokenList">A list of Tokens in postfix notation.</param>
        /// <returns>The result of the expression.</returns>
        private decimal EvaluatePostfixTokens(IList<Token> tokenList)
        {
            Stack<Token> stack = new Stack<Token>();

            foreach (Token token in tokenList)
            {
                Token result;

                if (token is ValueToken)
                {
                    result = token;
                }
                else if (token is UnaryOperatorToken unaryOp)
                {
                    decimal value = PopValueToken(stack).Value;
                    result = new ValueToken(unaryOp.Execute(value));
                }
                else if (token is BinaryOperatorToken binaryOp)
                {
                    decimal rightValue = PopValueToken(stack).Value;
                    decimal leftValue = PopValueToken(stack).Value;
                    result = new ValueToken(binaryOp.Execute(leftValue, rightValue));
                }
                else
                {
                    throw new NotImplementedException();
                }

                stack.Push(result);
            }

            return PopValueToken(stack).Value;
        }

        /// <summary>
        /// Returns the Token from the top of the stack if it's of type Token.Value.
        /// Otherwise, an exception is thrown.
        /// </summary>
        /// <param name="tokens">A Stack of Tokens.</param>
        /// <returns>The topmost Value Token.</returns>
        private ValueToken PopValueToken(Stack<Token> tokens)
        {
            if (tokens.Count == 0 || !(tokens.Peek() is ValueToken))
            {
                throw new InvalidExpressionException();
            }

            return (ValueToken)tokens.Pop();
        }
    }
}
