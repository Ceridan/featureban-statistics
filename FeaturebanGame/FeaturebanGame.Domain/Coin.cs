using System;

namespace FeaturebanGame.Domain
{
    public interface ICoin
    {
        CoinDropResult Drop();
    }

    public struct Coin : ICoin
    {
        private static readonly Random _random = new Random();

        public CoinDropResult Drop()
        {
            return (CoinDropResult)_random.Next(0, 2);
        }
    }
}