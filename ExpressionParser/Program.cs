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
                        IList<Token> tokens = parser.ParseExpression(expression);
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

        static void LogTokens(IList<Token> tokens)
        {
            foreach (Token token in tokens)
            {
                Console.WriteLine(token.Type + (token.Type == TokenType.Value ? " " + token.Value : ""));
            }
        }
    }
}
