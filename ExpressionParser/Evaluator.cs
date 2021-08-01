using ExpressionParser.Tokens;
using System;
using System.Collections.Generic;

namespace ExpressionParser
{
    /// <summary>
    /// Evaluates mathematical expressions represented by lists of Tokens.
    /// </summary>
    class Evaluator
    {
        /// <summary>
        /// Evaluates a TokenCollection representing a mathematical
        /// expression and returns the result.
        /// </summary>
        /// <param name="tokens">The TokenCollection to be evaluated.</param>
        /// <returns>The result of the expression.</returns>
        public decimal Evaluate(TokenCollection tokens)
        {
            Stack<Token> stack = new Stack<Token>();

            foreach (Token token in tokens.AsPostfix())
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
