using FeaturebanGame.Domain;
using NUnit.Framework;

namespace FeatureGame.Domain.Tests.DSL
{
    [TestFixture]
    public class WipColumnTests
    {
        [Test]
        public void ShouldPutCardIntoCardCollection_WhenAddCard()
        {
            var wip = new WipColumn(limit: 0);

            wip.AddCard(new Card());

            Assert.AreEqual(1, wip.Cards.Count);
        }
    }
}