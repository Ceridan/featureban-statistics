using System;
using System.IO;
using FeaturebanGame.Domain;

namespace FeaturebanGame.Runner
{
    class Program
    {
        private static readonly int[] playersCount = {3, 5, 10};
        private static readonly int[] turnsCount = {15, 20};
        private static readonly int[] wipLimitCount = {0, 1, 2, 3, 4, 5};
        private static int gamesCount = 1000;

        static void Main(string[] args)
        {
            foreach (var turns in turnsCount)
            {
                foreach (var players in playersCount)
                {
                    foreach (var wipLimit in wipLimitCount)
                    {
                        double cardsDone = 0;
                        for (var i = 0; i < gamesCount; i++)
                        {
                            var game = new Game(new Coin(), playerCount: players, wipLimit: wipLimit, turnCount: turns);
                            cardsDone += game.Play();
                        }

                        cardsDone /= gamesCount;
                        File.AppendAllText("result.txt", $"{cardsDone};");
                    }

                    File.AppendAllText("result.txt", Environment.NewLine);
                }
            }
        }
    }
}