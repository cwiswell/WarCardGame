using System;

namespace WarCardGame.Models
{
    internal class Card : IComparable
    {
        public Card() { }

        public Card(CardSuiteEnum suite, CardValueEnum value)
        {
            Type = suite;
            Value = value;
        }

        public CardSuiteEnum Type { get; set; }
        public CardValueEnum Value { get; set; }

        public static bool operator ==(Card x, Card y) => x?.Value == y?.Value;

        public static bool operator !=(Card x, Card y) => x?.Value != y?.Value;

        public static bool operator >(Card x, Card y) => x?.Value < y?.Value;

        public static bool operator <(Card x, Card y) => x?.Value > y?.Value;

        public static bool operator >=(Card x, Card y) => x?.Value >= y?.Value;

        public static bool operator <=(Card x, Card y) => x?.Value <= y?.Value;

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            var otherCard = obj as Card;
            if (otherCard != null)
                return this.Value.CompareTo(otherCard.Value);
            else
                throw new ArgumentException("Object is not a Card");
        }

        public override bool Equals(object obj)
        {
            return obj is Card card &&
                   Type == card.Type &&
                   Value == card.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Value);
        }
    }
}
