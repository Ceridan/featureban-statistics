using FeaturebanGame.Domain;

namespace FeatureGame.Domain.Tests.DSL
{
    public struct TwoTailCoin : ICoin
    {
        public CoinFlipResult Flip()
        {
            return CoinFlipResult.Tail;
        }
    }
}