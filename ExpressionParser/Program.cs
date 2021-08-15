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
            TokenParser parser = new TokenParser();
            Evaluator evaluator = new Evaluator();

            string expression;

            do
            {
                Console.WriteLine("Enter an expression to be evaluated:");
                expression = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(expression))
                {
                    try
                    {
                        TokenCollection tokens = parser.ParseExpression(expression);
                        Console.WriteLine("    = " + evaluator.Evaluate(tokens) + "\n");
                    }
                    catch
                    {
                        Console.WriteLine("Invalid expression.");
                    }
                }
            }
            while (expression != "");
        }
    }
}
