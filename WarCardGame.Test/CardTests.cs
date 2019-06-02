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

        [Fact]
        public void TwoCardsAreEqual()
        {
            var newCard1 = new Card(CardSuiteEnum.Heart, CardValueEnum.Queen);
            var newCard2 = new Card(CardSuiteEnum.Heart, CardValueEnum.Queen);

            Assert.True(newCard1.Equals(newCard2));
        }

        [Fact]
        public void TwoCardsSuitesAreNotEqual()
        {
            var newCard1 = new Card(CardSuiteEnum.Heart, CardValueEnum.Queen);
            var newCard2 = new Card(CardSuiteEnum.Diamond, CardValueEnum.Queen);

            Assert.False(newCard1.Equals(newCard2));
        }

        [Fact]
        public void TwoCardsValuesAreNotEqual()
        {
            var newCard1 = new Card(CardSuiteEnum.Heart, CardValueEnum.Queen);
            var newCard2 = new Card(CardSuiteEnum.Heart, CardValueEnum.Two);

            Assert.False(newCard1.Equals(newCard2));
        }

        [Fact]
        public void TwoCardsValuesAndSuitesAreNotEqual()
        {
            var newCard1 = new Card(CardSuiteEnum.Heart, CardValueEnum.Queen);
            var newCard2 = new Card(CardSuiteEnum.Diamond, CardValueEnum.Two);

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
    }
}
