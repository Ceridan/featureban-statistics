using FeaturebanGame.Domain;
using NUnit.Framework;

namespace FeatureGame.Domain.Tests
{
    [TestFixture]
    public class BacklogColumnTests
    {
        [Test]
        public void ShouldReturnSequenceIdNumbers_WhenGenerateNewCards()
        {
            var player = new Player(new Board(0));
            var backlog = new BacklogColumn();

            var card1 = backlog.GenerateNewCardForPlayer(player);
            var card2 = backlog.GenerateNewCardForPlayer(player);

            Assert.AreEqual(1, card1.Id);
            Assert.AreEqual(2, card2.Id);
        }
    }
}