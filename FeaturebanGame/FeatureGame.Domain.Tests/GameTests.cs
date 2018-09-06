using FeatureGame.Domain.Tests.DSL;
using NUnit.Framework;

namespace FeatureGame.Domain.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void WhenOnePlayerWithAllTailsPlayThreeTurns_ShouldBeOneCardInDone()
        {
            var coin = new TwoTailCoin();
            var game = Create.Game
                .WithPlayers(new [] { "MK" })
                .WithTurns(3)
                .WithCoin(coin)
                .Please();

            var cardsInDone = game.Play();

            Assert.AreEqual(1, cardsInDone);
        }

        [Test]
        public void WhenAnyAmountOfPlayersWithAllHeadsPlayAnyAmountOfTurns_ShouldBeZeroCardsInDone()
        {var coin = new TwoHeadCoin();
            var game = Create.Game
                .WithPlayers(new [] { "MK", "NS", "AB", "DP", "AM" })
                .WithTurns(10)
                .WithCoin(coin)
                .Please();

            var cardsInDone = game.Play();

            Assert.AreEqual(0, cardsInDone);
        }
    }
}