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
        public void WhenPlayerDropsTailAndTestLimitIsNotReached_ShouldMovePlayerCardToTestFromDev()
        {
            var board = Create.Board(@"
| Backlog |  Dev     | Test     | Done |
|         |  [MK  ]  |          |    0 |
");
            var mikhail = Create.Player
                .WithName("MK")
                .WithBoard(board)
                .Please();

            mikhail.Play(CoinDropResult.Tail);

            AssertBoard(board, @"
| Backlog |   Dev    |   Test   | Done |
|         |          |  [MK  ]  |    0 |
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