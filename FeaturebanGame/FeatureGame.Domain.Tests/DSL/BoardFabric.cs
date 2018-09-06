using System;
using System.Collections.Generic;
using FeaturebanGame.Domain;

namespace FeatureGame.Domain.Tests.DSL
{
    public static class BoardFabric
    {
//| Backlog |  Dev (5) | Test (5) | Done |
//|         |  [MK  ]  |  [NS B]  |   25 |
//|         |          |  [MK  ]  |      |
        public static Board CreateBoard(string boardSketch)
        {
            return CreateBoard(boardSketch, new Coin());
        }

        public static Board CreateBoard(string boardSketch, ICoin coin)
        {
            var players = new List<Player>();

            var lines = boardSketch.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries
            );

            var wipLimit = ExtractWipLimitFromHeaderLine(lines[0]);
            var board = new Board(wipLimit);
            board.Done.CardCount = ExtractDoneCountFromFirstLine(lines[1]);

            for (var i = 1; i < lines.Length; i++)
            {
                var columns = lines[i].Split(
                    new[] { "|" },
                    StringSplitOptions.RemoveEmptyEntries
                );

                var devCard = ParseCard(columns[1]);
                if (!string.IsNullOrEmpty(devCard.playerName))
                {
                    var player = new Player(devCard.playerName, board, coin);
                    board.Dev.AddCard(CardFabric.CreateCard(player, devCard.cardState));
                    players.Add(player);
                }

                var testCard = ParseCard(columns[2]);
                if (!string.IsNullOrEmpty(testCard.playerName))
                {
                    var player = new Player(testCard.playerName, board, coin);
                    board.Test.AddCard(CardFabric.CreateCard(player, devCard.cardState));
                    players.Add(player);
                }
            }

            return board;
        }

        private static (string playerName, CardState cardState) ParseCard(string cardColumn)
        {
            var p1 = cardColumn.IndexOf("[");

            if (p1 == -1)
                return (null, CardState.Available);

            var p2 = cardColumn.IndexOf("]", p1);

            var cardInfoLines = cardColumn
                .Substring(p1 + 1, p2 - p1 - 1)
                .Split(
                    new[] {' '},
                    StringSplitOptions.RemoveEmptyEntries
                );

            return cardInfoLines.Length == 1
                ? (cardInfoLines[0], CardState.Available)
                : (cardInfoLines[0], CardState.Blocked);
        }

        private static int ExtractWipLimitFromHeaderLine(string headerLine)
        {
            var p1 = headerLine.IndexOf("(");

            if (p1 == -1)
                return 0;

            var p2 = headerLine.IndexOf(")", p1);

            return int.Parse(headerLine.Substring(p1 + 1, p2 - p1 - 1));
        }

        private static int ExtractDoneCountFromFirstLine(string firstLine)
        {
            var columns = firstLine.Split(
                new[] { "|" },
                StringSplitOptions.RemoveEmptyEntries
            );

            var doneColumn = columns[columns.Length - 1];
            var doneCount = doneColumn.Replace("\t", "").Replace(" ", "");

            return string.IsNullOrEmpty(doneCount) ? 0 : int.Parse(doneCount);
        }
    }
}