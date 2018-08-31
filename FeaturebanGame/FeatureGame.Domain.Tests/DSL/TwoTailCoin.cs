using FeaturebanGame.Domain;

namespace FeatureGame.Domain.Tests.DSL
{
    public struct TwoTailCoin : ICoin
    {
        public CoinDropResult Drop()
        {
            return CoinDropResult.Tail;
        }
    }
}