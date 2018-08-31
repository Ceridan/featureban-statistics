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
                .WithPlayers(1)
                .WithTurns(3)
                .WithCoin(coin)
                .Please();

            var cardsInDone = game.Play();

            Assert.AreEqual(1, cardsInDone);
        }
    }
}