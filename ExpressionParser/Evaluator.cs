using ExpressionParser.Tokens;
using System;
using System.Collections.Generic;

namespace ExpressionParser
{
    /// <summary>
    /// Evaluates a mathematical expression represented by a TokenCollection.
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
            var values = new Stack<decimal>();

            try
            {
                foreach (Token token in tokens.AsPostfix())
                {
                    decimal result;

                    switch (token)
                    {
                        case ValueToken value:
                            result = value.Evaluate();
                            break;

                        case UnaryOperatorToken unaryOp:
                            result = unaryOp.Evaluate(values.Pop());
                            break;

                        case BinaryOperatorToken binaryOp:
                            decimal rightValue = values.Pop();
                            decimal leftValue = values.Pop();
                            result = binaryOp.Evaluate(leftValue, rightValue);
                            break;

                        default:
                            throw new InvalidExpressionException($"Token '{token}' cannot be evaluated");
                    }

                    values.Push(result);
                }

                return values.Pop();
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidExpressionException(ex);
            }
        }
    }
}
