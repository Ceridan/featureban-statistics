using System;

namespace FeaturebanGame.Domain
{
    public struct Coin
    {
        private static readonly Random Random = new Random();

        public static CoinDropResult Drop()
        {
            return (CoinDropResult)Random.Next(0, 1);
        }
    }
}