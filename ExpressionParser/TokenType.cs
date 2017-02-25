using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionParser
{
    /// <summary>
    /// Defines constants for types of expression Tokens.
    /// </summary>
    enum TokenType
    {
        Invalid = 0,
        Value = 1,
        LeftParenthesis = 2,
        RightParenthesis = 3,
        Add = 4,
        Subtract = 5,
        Multiply = 6,
        Divide = 7
    }
}