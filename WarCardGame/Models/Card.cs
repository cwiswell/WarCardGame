namespace WarCardGame.Models
{
    internal class Card
    {
        public Card(CardSuiteEnum suite, CardValueEnum value)
        {
            Type = suite;
            Value = value;
        }

        public CardSuiteEnum Type { get; set; }
        public CardValueEnum Value { get; set; }
    }
}
