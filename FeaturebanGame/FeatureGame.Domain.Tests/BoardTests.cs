using System.Linq;
using FeaturebanGame.Domain;
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

	    [Test]
	    public void GetOrderedPlayerCards_ShouldReturnOrderedCardsForPlayer()
	    {
		    var board = Create.Board
			    .WithCardOnFirstWipColumn(new Card { Id = 1 })
			    .WithCardOnSecondWipColumn(new Card { Id = 2 })
			    .Please();
		    var player = Create.Player
			    .WithBoard(board)
			    .AssignAllCardsOnBoardToPlayer()
			    .Please();

		    var cards = board.GetOrderedPlayerCards(player);

		    Assert.AreEqual(2, cards.First().Id);
		    Assert.AreEqual(1, cards.Last().Id);
	    }

	    [Test]
	    public void GetOrderedCards_ShouldReturnOrderedCards()
	    {
		    var board = Create.Board
			    .WithCardOnFirstWipColumn(new Card { Id = 1 })
			    .WithCardOnSecondWipColumn(new Card { Id = 2 })
			    .Please();

		    var cards = board.GetOrderedCards();

		    Assert.AreEqual(2, cards.First().Id);
		    Assert.AreEqual(1, cards.Last().Id);
	    }
    }
}