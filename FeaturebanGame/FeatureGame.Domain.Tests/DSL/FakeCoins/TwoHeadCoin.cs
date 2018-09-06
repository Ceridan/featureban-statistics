using FeaturebanGame.Domain;

namespace FeatureGame.Domain.Tests.DSL
{
    public struct TwoHeadCoin : ICoin
    {
        public CoinFlipResult Flip()
        {
            return CoinFlipResult.Head;
        }
    }
}