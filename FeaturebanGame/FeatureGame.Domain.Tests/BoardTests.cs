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
        public void WhenGetOrderedPlayerCards_ShouldReturnCardsForSelectedPlayer()
        {
	        var board = Create.Board
		        .WithAvailableCard()
		        .WithBlockedCard()
		        .Please();
	        var player1 = Create.Player
				.WithId(1)
		        .WithBoard(board)
		        .AssignAllAvailableCardsOnBoardToPlayer()
		        .Please();
	        var player2 = Create.Player
		        .WithId(2)
		        .WithBoard(board)
		        .AssignAllBlockedCardsOnBoardToPlayer()
		        .Please();

	        var player1Cards = board.GetOrderedPlayerCards(player1);

	        Assert.AreEqual(player1, player1Cards.Single().Player);
        }

	    [Test]
	    public void WhenGetOrderedPlayerCards_ShouldReturnOrderedCardsForPlayer()
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
	    public void WhenGetOrderedCards_ShouldReturnOrderedCards()
	    {
		    var board = Create.Board
			    .WithCardOnFirstWipColumn(new Card { Id = 1 })
			    .WithCardOnSecondWipColumn(new Card { Id = 2 })
			    .Please();

		    var cards = board.GetOrderedCards();

		    Assert.AreEqual(2, cards.First().Id);
		    Assert.AreEqual(1, cards.Last().Id);
	    }

	    [Test]
	    public void WhenBlockCard_ShouldBlockCard()
	    {
		    var board = Create.Board.Please();
		    var card = new Card {State = CardState.Available};

		    board.BlockCard(card);

		    Assert.AreEqual(CardState.Blocked, card.State);
	    }

	    [Test]
	    public void WhenUnblockCard_ShouldMakeCardAvailable()
	    {
		    var board = Create.Board.Please();
		    var card = new Card {State = CardState.Blocked};

		    board.UnblockCard(card);

		    Assert.AreEqual(CardState.Available, card.State);
	    }

	    [Test]
	    public void WhenAddNewCard_ShouldGenerateNewAvailableCard()
	    {
		    var board = Create.Board.Please();
		    var player = Create.Player.WithBoard(board).Please();

		    board.AddNewCardFor(player);

		    var card = board.Wips.First().Cards.Single();
		    Assert.AreEqual(CardState.Available, card.State);
	    }

	    [Test]
	    public void WhenAddNewCard_ShouldGenerateNewCardForSelectedPlayer()
	    {
		    var board = Create.Board.Please();
		    var player = Create.Player.WithBoard(board).Please();

		    board.AddNewCardFor(player);

		    var card = board.Wips.First().Cards.Single();
		    Assert.AreEqual(player, card.Player);
	    }

	    [Test]
	    public void WhenAddNewCard_ShouldNotGenerateNewCardIfWipLimitIsReached()
	    {
		    var board = Create.Board
			    .WithWipLimit(1)
			    .WithCardOnFirstWipColumn(new Card())
			    .Please();
		    var player = Create.Player
			    .WithBoard(board)
			    .Please();

		    board.AddNewCardFor(player);

		    Assert.AreEqual(1, board.Wips.First().Cards.Count);
	    }

	    [Test]
	    public void WhenMoveCard_ShouldNotMoveBlockedCard()
	    {
		    var card = new Card {State = CardState.Blocked};
		    var board = Create.Board
			    .WithCardOnFirstWipColumn(card)
			    .Please();

		    board.MoveCard(card);

		    Assert.AreEqual(1, board.Wips.First().Cards.Count);
	    }

	    [Test]
	    public void WhenMoveAvailableCardFromFirstWipColumn_ShouldMovedToSecondWipColumn()
	    {
		    var card = new Card {State = CardState.Available};
		    var board = Create.Board
			    .WithCardOnFirstWipColumn(card)
			    .Please();

		    board.MoveCard(card);

		    Assert.AreEqual(0, board.Wips.First().Cards.Count);
		    Assert.AreEqual(1, board.Wips.Last().Cards.Count);
	    }

	    [Test]
	    public void WhenMoveAvailableCardFromSecondWipColumn_ShouldMovedToDoneColumn()
	    {
		    var card = new Card {State = CardState.Available};
		    var board = Create.Board
			    .WithCardOnSecondWipColumn(card)
			    .Please();

		    board.MoveCard(card);

		    Assert.AreEqual(0, board.Wips.Last().Cards.Count);
		    Assert.AreEqual(1, board.DoneColumn.CardCount);
	    }

	    [Test]
	    public void WhenMoveAvailableCardFromFirstWipColumn_ShouldNotMoveIfSecondWipColumnReachedLimit()
	    {
		    var card = new Card {State = CardState.Available};
		    var board = Create.Board
				.WithWipLimit(1)
			    .WithCardOnFirstWipColumn(card)
			    .WithCardOnSecondWipColumn(new Card())
			    .Please();

		    board.MoveCard(card);

		    Assert.AreEqual(1, board.Wips.First().Cards.Count);
		    Assert.AreEqual(1, board.Wips.Last().Cards.Count);
	    }
    }
}