﻿namespace ExpressionParser.Tokens
{
    class ValueToken : Token
    {
        public decimal Value { get; }

        public ValueToken(decimal value)
            : base(0)
        {
            Value = value;
        }
    }
}
