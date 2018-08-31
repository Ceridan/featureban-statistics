using System.Linq;
using FeatureGame.Domain.Tests.DSL;
using NUnit.Framework;

namespace FeatureGame.Domain.Tests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void GetOrderedPlayerCards_ShouldReturnCardsForSelectedPlayer()
        {
	        var board = Create.Board
		        .WithAvailableCard()
		        .WithBlockedCard()
		        .Please();
	        var player1 = Create.Player
		        .WithBoard(board)
		        .AssignAllAvailableCardsOnBoardToPlayer()
		        .Please();
	        var player2 = Create.Player
		        .WithBoard(board)
		        .AssignAllBlockedCardsOnBoardToPlayer()
		        .Please();

	        var player1Cards = board.GetOrderedPlayerCards(player1);

	        Assert.AreEqual(player1, player1Cards.Single().Player);
        }

    }
}