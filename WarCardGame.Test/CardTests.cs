using WarCardGame.Models;
using Xunit;

namespace WarCardGame.Test
{
    public class CardTests
    {
        [Fact]
        public void EmptyCardToString()
        {
            var newCard = new Card();

            var cardString = newCard.ToString();

            Assert.True(cardString == "Two of Spade");
        }

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
    }
}
