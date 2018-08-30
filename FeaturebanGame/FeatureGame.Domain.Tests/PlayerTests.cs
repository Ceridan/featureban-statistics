using System.Linq;
using FeaturebanGame.Domain;
using FeatureGame.Domain.Tests.DSL;
using NUnit.Framework;

namespace FeatureGame.Domain.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void ShouldCreateCardForPlayer_WhenPlayerDoWorkWithCoinHeadAndEmptyBoard()
        {
            var board = Create.Board.Please();
            var player = Create.Player.WithBoard(board).Please();

            player.DoWork(CoinDropResult.Head);

            var playerCards = board.GetOrderedPlayerCards(player);
            Assert.AreEqual(1, playerCards.Count);
        }
        
        [Test]
        public void ShouldCreateCardForPlayer_WhenPlayerDoWorkWithCoinTailAndEmptyBoard()
        {
            var board = Create.Board.Please();
            var player = Create.Player.WithBoard(board).Please();

            player.DoWork(CoinDropResult.Tail);

            var playerCards = board.GetOrderedPlayerCards(player);
            Assert.AreEqual(1, playerCards.Count);
        }
        
        [Test]
        public void ShouldCreateCardForPlayer_WhenPlayerDoWorkWithCoinHeadAndBoardWithBlockedCard()
        {
            var board = Create.Board.WithBlockedCard().Please();
            var player = Create.Player.WithBoard(board).AssignAllCardsOnBoardToPlayer().Please();

            player.DoWork(CoinDropResult.Head);

            var playerCards = board.GetOrderedPlayerCards(player);
            Assert.AreEqual(2, playerCards.Count);
        }
        
        [Test]
        public void ShouldCreateCardForPlayer_WhenPlayerDoWorkWithCoinTailAndBoardWithBlockedCard()
        {
            var board = Create.Board.WithBlockedCard().Please();
            var player = Create.Player.WithBoard(board).AssignAllCardsOnBoardToPlayer().Please();

            player.DoWork(CoinDropResult.Tail);

            var playerCard = board.GetOrderedPlayerCards(player).Single();
            Assert.AreEqual(CardState.Available, playerCard.State);
        }
    }
}