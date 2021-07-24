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
        Divide = 7,
        Negate = 8
    }
}