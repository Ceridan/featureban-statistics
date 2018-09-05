﻿using System;
using System.IO;
using System.Linq;
using FeaturebanGame.Domain;

namespace FeaturebanGame.Runner
{
    class Program
    {
        private const string OutputFileName = "result.txt";
        private static readonly int _gamesCount = 1000;
        private static readonly int[] turnsCount = {15, 20};
        private static readonly int[] wipLimitCount = {0, 1, 2, 3, 4, 5};
        private static readonly int[] playersCount = {3, 5, 10};
        private static readonly string[] playerNames = { "NS", "MK", "AB", "AM", "YP", "SZ", "PP", "DA", "DB", "DP" };

        static void Main(string[] args)
        {
//            foreach (var turns in turnsCount)
//            {
//                foreach (var players in playersCount)
//                {
//                    foreach (var wipLimit in wipLimitCount)
//                    {
//                        double cardsDone = 0;
//
//                        for (var i = 0; i < _gamesCount; i++)
//                        {
//                            var game = new Game(
//                                playerNames: playerNames.Take(players),
//                                turnsCount: turns,
//                                wipLimit: wipLimit,
//                                coin: new Coin()
//                            );
//                            cardsDone += game.Play();
//                        }
//
//                        cardsDone /= _gamesCount;
//                        File.AppendAllText(OutputFileName, $"{cardsDone};");
//                    }
//
//                    File.AppendAllText(OutputFileName, Environment.NewLine);
//                }
//            }

            var coin = new Coin();
            var board = new Board(0);
            var mikhail = new Player("MK", board, coin);
            var nikita = new Player("NS", board, coin);

            board.Dev.AddCard(CardFabric.CreateCard(mikhail, CardState.Available));
            board.Test.AddCard(CardFabric.CreateCard(nikita, CardState.Blocked));
            board.Test.AddCard(CardFabric.CreateCard(mikhail, CardState.Available));
            board.Done.CardCount = 25;

            Console.WriteLine(board);
        }
    }
}