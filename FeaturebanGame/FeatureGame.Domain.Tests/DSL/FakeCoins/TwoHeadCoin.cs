using FeaturebanGame.Domain;

namespace FeatureGame.Domain.Tests.DSL
{
    public struct TwoHeadCoin : ICoin
    {
        public CoinDropResult Drop()
        {
            return CoinDropResult.Head;
        }
    }
}