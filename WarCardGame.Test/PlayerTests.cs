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
    }
}
