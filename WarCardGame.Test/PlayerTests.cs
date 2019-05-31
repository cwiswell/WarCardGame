using WarCardGame.Models;
using Xunit;

namespace WarCardGame.Test
{
    public class PlayerTests
    {
        [Fact]
        public void NewPlayerIsActive()
        {
            var player = new Player();

            Assert.True(player.ActivePlayer);
        }

        [Fact]
        public void NewPlayerHasEmptyHand()
        {
            var player = new Player();

            Assert.False(player.AnyCardsLeft());
        }

        [Fact]
        public void NewPlayerIsInactiveAfterCheck()
        {
            var player = new Player();

            player.CheckIfStillActive();

            Assert.False(player.ActivePlayer);
        }

        [Fact]
        public void DrawCardFromEmptyHand()
        {
            var player = new Player();

            var drawnCard = player.DrawCard();

            Assert.Null(drawnCard);
        }

        [Fact]
        public void AddCardToPlayerHand()
        {
            var player = new Player();

            var cardToAdd = new Card(CardSuiteEnum.Clubs, CardValueEnum.Ace);
            player.AddCard(cardToAdd);

            Assert.True(player.GetHandCount() == 1);
        }
    }
}
