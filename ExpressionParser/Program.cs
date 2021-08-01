using ExpressionParser.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser
{
    /// <summary>
    /// A simple parser and evaluator for mathemetical expressions.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string expression;
            TokenParser parser = new TokenParser();
            Evaluator evaluator = new Evaluator();

            do
            {
                Console.WriteLine("Enter an expression to be evaluated:");
                expression = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(expression))
                {
                    try
                    {
                        TokenCollection tokens = parser.ParseExpression(expression);
                        LogTokens(tokens);
                        Console.WriteLine("    = " + evaluator.Evaluate(tokens));
                    }
                    catch
                    {
                        Console.WriteLine("Invalid expression.");
                    }
                }
            }
            while (expression != "");
        }

        static void LogTokens(TokenCollection tokens)
        {
            foreach (Token token in tokens.AsInfix())
            {
                Console.Write(token.ToString());
                Console.WriteLine();
            }
        }
    }
}
