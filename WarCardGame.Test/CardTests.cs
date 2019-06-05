using WarCardGame.Models;
using Xunit;

namespace WarCardGame.Test
{
    public class CardTests
    {
        [Fact]
        public void CardToString()
        {
            var newCard = new Card(CardSuiteEnum.Heart, CardValueEnum.Queen);

            var cardString = newCard.ToString();

            Assert.True(cardString == "Queen of Heart");
        }

        [Theory]
        [InlineData(CardSuiteEnum.Heart, CardValueEnum.Queen, CardSuiteEnum.Heart, CardValueEnum.Queen)]
        [InlineData(CardSuiteEnum.Diamond, CardValueEnum.Ace, CardSuiteEnum.Diamond, CardValueEnum.Ace)]
        [InlineData(CardSuiteEnum.Spade, CardValueEnum.Two, CardSuiteEnum.Spade, CardValueEnum.Two)]
        [InlineData(CardSuiteEnum.Clubs, CardValueEnum.Jack, CardSuiteEnum.Clubs, CardValueEnum.Jack)]
        internal void TwoCardsAreEqual(CardSuiteEnum suite1, CardValueEnum value1, CardSuiteEnum suite2, CardValueEnum value2)
        {
            var newCard1 = new Card(suite1, value1);
            var newCard2 = new Card(suite2, value2);

            Assert.True(newCard1.Equals(newCard2));
        }

        [Theory]
        [InlineData(CardSuiteEnum.Heart, CardValueEnum.Queen, CardSuiteEnum.Diamond, CardValueEnum.Queen)]
        [InlineData(CardSuiteEnum.Diamond, CardValueEnum.Ace, CardSuiteEnum.Spade, CardValueEnum.Ace)]
        [InlineData(CardSuiteEnum.Spade, CardValueEnum.Two, CardSuiteEnum.Diamond, CardValueEnum.Two)]
        [InlineData(CardSuiteEnum.Clubs, CardValueEnum.Jack, CardSuiteEnum.Heart, CardValueEnum.Jack)]
        internal void TwoCardsSuitesAreNotEqual(CardSuiteEnum suite1, CardValueEnum value1, CardSuiteEnum suite2, CardValueEnum value2)
        {
            var newCard1 = new Card(suite1, value1);
            var newCard2 = new Card(suite2, value2);

            Assert.False(newCard1.Equals(newCard2));
        }

        [Theory]
        [InlineData(CardSuiteEnum.Heart, CardValueEnum.Queen, CardSuiteEnum.Heart, CardValueEnum.King)]
        [InlineData(CardSuiteEnum.Diamond, CardValueEnum.Ace, CardSuiteEnum.Diamond, CardValueEnum.Nine)]
        [InlineData(CardSuiteEnum.Spade, CardValueEnum.Two, CardSuiteEnum.Spade, CardValueEnum.Ace)]
        [InlineData(CardSuiteEnum.Heart, CardValueEnum.Jack, CardSuiteEnum.Heart, CardValueEnum.Ten)]
        internal void TwoCardsValuesAreNotEqual(CardSuiteEnum suite1, CardValueEnum value1, CardSuiteEnum suite2, CardValueEnum value2)
        {
            var newCard1 = new Card(suite1, value1);
            var newCard2 = new Card(suite2, value2);

            Assert.False(newCard1.Equals(newCard2));
        }

        [Theory]
        [InlineData(CardSuiteEnum.Heart, CardValueEnum.Queen, CardSuiteEnum.Diamond, CardValueEnum.King)]
        [InlineData(CardSuiteEnum.Clubs, CardValueEnum.Ace, CardSuiteEnum.Diamond, CardValueEnum.Nine)]
        [InlineData(CardSuiteEnum.Spade, CardValueEnum.Two, CardSuiteEnum.Heart, CardValueEnum.Ace)]
        [InlineData(CardSuiteEnum.Heart, CardValueEnum.Jack, CardSuiteEnum.Diamond, CardValueEnum.Ten)]
        [InlineData(CardSuiteEnum.Heart, CardValueEnum.Two, CardSuiteEnum.Diamond, CardValueEnum.King)]
        [InlineData(CardSuiteEnum.Clubs, CardValueEnum.Three, CardSuiteEnum.Diamond, CardValueEnum.Nine)]
        [InlineData(CardSuiteEnum.Spade, CardValueEnum.Five, CardSuiteEnum.Heart, CardValueEnum.Ace)]
        [InlineData(CardSuiteEnum.Heart, CardValueEnum.Eight, CardSuiteEnum.Diamond, CardValueEnum.Ten)]
        internal void TwoCardsValuesAndSuitesAreNotEqual(CardSuiteEnum suite1, CardValueEnum value1, CardSuiteEnum suite2, CardValueEnum value2)
        {
            var newCard1 = new Card(suite1, value1);
            var newCard2 = new Card(suite2, value2);

            Assert.False(newCard1.Equals(newCard2));
        }

        [Fact]
        public void TwoCardsEqualOperator()
        {
            var newCard1 = new Card(CardSuiteEnum.Heart, CardValueEnum.Queen);
            var newCard2 = new Card(CardSuiteEnum.Heart, CardValueEnum.Queen);

            Assert.True(newCard1 == newCard2);
        }

        [Fact]
        public void TwoCardsValuesEqualOperator()
        {
            var newCard1 = new Card(CardSuiteEnum.Heart, CardValueEnum.Queen);
            var newCard2 = new Card(CardSuiteEnum.Diamond, CardValueEnum.Queen);

            Assert.True(newCard1 == newCard2);
        }

        [Fact]
        public void TwoCardsValuesDifferentEqualOperator()
        {
            var newCard1 = new Card(CardSuiteEnum.Heart, CardValueEnum.Queen);
            var newCard2 = new Card(CardSuiteEnum.Heart, CardValueEnum.Two);

            Assert.False(newCard1 == newCard2);
        }

        [Fact]
        public void TwoCardsValuesAndSuiteDifferentEqualOperator()
        {
            var newCard1 = new Card(CardSuiteEnum.Heart, CardValueEnum.Queen);
            var newCard2 = new Card(CardSuiteEnum.Diamond, CardValueEnum.Two);

            Assert.False(newCard1 == newCard2);
        }


        [Fact]
        public void TwoCardsNotEqualOperator()
        {
            var newCard1 = new Card(CardSuiteEnum.Heart, CardValueEnum.Queen);
            var newCard2 = new Card(CardSuiteEnum.Heart, CardValueEnum.Queen);

            Assert.False(newCard1 != newCard2);
        }

        [Fact]
        public void TwoCardsValuesNotEqualOperator()
        {
            var newCard1 = new Card(CardSuiteEnum.Heart, CardValueEnum.Queen);
            var newCard2 = new Card(CardSuiteEnum.Diamond, CardValueEnum.Queen);

            Assert.False(newCard1 != newCard2);
        }

        [Fact]
        public void TwoCardsValuesDifferentNotEqualOperator()
        {
            var newCard1 = new Card(CardSuiteEnum.Heart, CardValueEnum.Queen);
            var newCard2 = new Card(CardSuiteEnum.Heart, CardValueEnum.Two);

            Assert.True(newCard1 != newCard2);
        }

        [Fact]
        public void TwoCardsValuesAndSuiteDifferentNotEqualOperator()
        {
            var newCard1 = new Card(CardSuiteEnum.Heart, CardValueEnum.Queen);
            var newCard2 = new Card(CardSuiteEnum.Diamond, CardValueEnum.Two);

            Assert.True(newCard1 != newCard2);
        }
    }
}
