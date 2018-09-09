using System;
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
        public void WhenPlayerDropsTailAndTestLimitIsNotReached_ShouldMoveAvailablePlayerCardFromDevToTest()
        {
            var board = Create.Board(@"
| Backlog |  Dev     | Test     | Done |
|         |  [MK  ]  |          |    0 |
");
            var mikhail = Create.Player
                .WithName("MK")
                .Please();

            board.MakeTurnFor(mikhail, CoinFlipResult.Tail);

            AssertBoard(board, @"
| Backlog |   Dev    |   Test   | Done |
|         |          |  [MK  ]  |    0 |
");
        }

        [Test]
        public void WhenPlayerDropsTailAndTestLimitIsNotReached_ShouldMoveAvailablePlayerCardFromTestToDone()
        {
            var board = Create.Board(@"
| Backlog |   Dev    |   Test   | Done |
|         |          |  [MK  ]  |    0 |
");
            var mikhail = Create.Player
                .WithName("MK")
                .Please();

            board.MakeTurnFor(mikhail, CoinFlipResult.Tail);

            AssertBoard(board, @"
| Backlog |   Dev    |   Test   | Done |
|         |          |          |    1 |
");
        }

        [Test]
        public void WhenPlayerDropsHeadAndDevLimitIsNotReached_ShouldBlockTheCardAndPullTheNewOne()
        {
            var board = Create.Board(@"
| Backlog |   Dev    |   Test   | Done |
|         |  [MK  ]  |          |    0 |
");
            var mikhail = Create.Player
                .WithName("MK")
                .Please();

            board.MakeTurnFor(mikhail, CoinFlipResult.Head);

            AssertBoard(board, @"
| Backlog |   Dev    |   Test   | Done |
|         |  [MK B]  |          |    0 |
|         |  [MK  ]  |          |      |
");
        }

        [Test]
        public void WhenPlayerDropsHeadAndDevLimitIsReached_ShouldBlockTheCard()
        {
            var board = Create.Board(@"
| Backlog |  Dev (1) | Test (1) | Done |
|         |  [MK  ]  |          |    0 |
");
            var mikhail = Create.Player
                .WithName("MK")
                .Please();

            board.MakeTurnFor(mikhail, CoinFlipResult.Head);

            AssertBoard(board, @"
| Backlog |  Dev (1) | Test (1) | Done |
|         |  [MK B]  |          |    0 |
");
        }

        [Test]
        public void WhenPlayerDropsTailAndHaveBlockedCard_ShouldUnblockCard()
        {
            var board = Create.Board(@"
| Backlog |   Dev    |   Test   | Done |
|         |  [MK B]  |          |    0 |
");
            var mikhail = Create.Player
                .WithName("MK")
                .Please();

            board.MakeTurnFor(mikhail, CoinFlipResult.Tail);

            AssertBoard(board, @"
| Backlog |   Dev    |   Test   | Done |
|         |  [MK  ]  |          |    0 |
");
        }

        [Test]
        public void WhenPlayerDropsTailAndCantPlayOwnCards_PlayerMoveOtherCards()
        {
            var board = Create.Board(@"
| Backlog |  Dev (1) | Test (1) | Done |
|         |  [MK  ]  |          |    0 |
");
            var nikita = Create.Player
                .WithName("NS")
                .Please();

            board.MakeTurnFor(nikita, CoinFlipResult.Tail);

            AssertBoard(board, @"
| Backlog |  Dev (1) | Test (1) | Done |
|         |          |  [MK  ]  |    0 |
");
        }

        [Test]
        public void WhenPlayerDropsTailAndCantPlayOwnCardsAndCantMoveCards_PlayerUnblockOtherCards()
        {
            var board = Create.Board(@"
| Backlog |  Dev (1) | Test (1) | Done |
|         |  [MK B]  |          |    0 |
");
            var nikita = Create.Player
                .WithName("NS")
                .Please();

            board.MakeTurnFor(nikita, CoinFlipResult.Tail);

            AssertBoard(board, @"
| Backlog |  Dev (1) | Test (1) | Done |
|         |  [MK  ]  |          |    0 |
");
        }

        [Test]
        public void WhenBoardIsEmptyAndPlayerPlayTurnWithAnyCoinFlipResult_ShouldPullNewCardToDev()
        {
            var board = Create.Board(@"
| Backlog |    Dev   |   Test   | Done |
|         |          |          |    0 |
");
            var mikhail = Create.Player
                .WithName("MK")
                .Please();

            var coinFlipResult = mikhail.FlipTheCoin(new Coin());
            board.MakeTurnFor(mikhail, coinFlipResult);

            AssertBoard(board, @"
| Backlog |   Dev    |   Test   | Done |
|         |  [MK  ]  |          |    0 |
");
        }

        private static void AssertBoard(Board board, string expectedBoardSketch)
        {
            var expected = expectedBoardSketch.Replace("\r\n", "").Replace("\n", "").Replace("\t", "").Replace(" ", "");
            var actual = board.ToString().Replace("\r\n", "").Replace("\n", "").Replace("\t", "").Replace(" ", "");
            Assert.AreEqual(expected, actual);
        }
    }
}