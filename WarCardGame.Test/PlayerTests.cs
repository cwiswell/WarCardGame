using System.Collections.Generic;
using WarCardGame.Models;
using Xunit;

namespace WarCardGame.Test
{
    public class PlayerTests
    {
        [Fact]
        public void NewPlayerIsActive()
        {
            var player = new Player(true,1);

            Assert.True(player.ActivePlayer);
        }

        [Fact]
        public void NewPlayerHasEmptyHand()
        {
            var player = new Player(true, 1);

            Assert.False(player.AnyCardsLeft());
        }

        [Fact]
        public void NewPlayerIsInactiveAfterCheck()
        {
            var player = new Player(true, 1);

            player.CheckIfStillActive();

            Assert.False(player.ActivePlayer);
        }

        [Fact]
        public void DrawCardFromEmptyHand()
        {
            var player = new Player(true, 1);

            var drawnCard = player.DrawCard();

            Assert.Null(drawnCard);
        }

        [Fact]
        public void AddCardToPlayerHand()
        {
            var player = new Player(true, 1);

            var cardToAdd = new Card(CardSuiteEnum.Clubs, CardValueEnum.Ace);
            player.AddCard(cardToAdd);

            Assert.True(player.GetHandCount() == 1);
        }

        [Fact]
        public void AddCardListToPlayerHand()
        {
            var player = new Player(true, 0);

            var cardList = new List<Card>
            {
                new Card(CardSuiteEnum.Clubs, CardValueEnum.Ace),
                new Card(CardSuiteEnum.Clubs, CardValueEnum.Two)
            };

            player.AddCards(cardList);

            Assert.True(player.GetHandCount() == 2);
        }
    }
}
