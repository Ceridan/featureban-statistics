using System;

namespace FeaturebanGame.Domain
{
    public interface ICoin
    {
        CoinFlipResult Flip();
    }

    public struct Coin : ICoin
    {
        private static readonly Random _random = new Random();

        public CoinFlipResult Flip()
        {
            return (CoinFlipResult)_random.Next(0, 2);
        }
    }
}