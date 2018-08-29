using System;

namespace FeaturebanGame.Domain
{
    public struct Coin
    {
        private static readonly Random _random = new Random();

        public CoinDropResult Drop()
        {
            return (CoinDropResult)_random.Next(0, 1);
        }
    }
}