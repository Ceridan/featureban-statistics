using FeaturebanGame.Domain;
using FeatureGame.Domain.Tests.DSL;
using NUnit.Framework;

namespace FeatureGame.Domain.Tests
{
    [TestFixture]
    public class BacklogColumnTests
    {
        [Test]
        public void WhenBacklogColumnGenerateNewCard_ShouldAssignItToPlayer()
        {
            var player = Create.Player.Please();
            var backlog = new BacklogColumn();

            var card = backlog.GenerateNewCardForPlayer(player);

            Assert.AreEqual(player, card.Player);
        }
    }
}