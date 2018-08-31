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

        [Test]
        public void ShouldNotPutCardIntoCardCollection_WhenAddCardAndWipColumnReachedTheLimit()
        {
            var wip = Create.WipColumn
                .WithLimit(1)
                .WithCard()
                .Please();

            wip.AddCard(new Card());

            Assert.AreEqual(1, wip.Cards.Count);
        }

        [Test]
        public void ShouldRemoveCardFromCardsCollection_WhenRemoveCardAttachedToColumn()
        {
            var card = new Card();
            var wip = Create.WipColumn
                .WithLimit(1)
                .WithCard(card)
                .Please();

            wip.RemoveCard(card);

            Assert.AreEqual(0, wip.Cards.Count);
        }
    }
}