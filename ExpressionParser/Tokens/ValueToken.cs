namespace ExpressionParser.Tokens
{
    public class ValueToken : Token
    {
        public decimal Value { get; }

        public ValueToken(decimal value)
            : base(value.ToString(), 0)
        {
            Value = value;
        }

        public decimal Evaluate()
        {
            return Value;
        }
    }
}
