using System;

namespace FeaturebanGame.Domain
{
    public interface ICoin
    {
        CoinDropResult Drop();
    }

    public struct Coin : ICoin
    {
        private static readonly Random Random = new Random();

        public CoinDropResult Drop()
        {
            return (CoinDropResult)Random.Next(0, 1);
        }
    }
}