using FeaturebanGame.Domain;

namespace FeatureGame.Domain.Tests.DSL
{
    public static class Create
    {
        public static PlayerBuilder Player => new PlayerBuilder();
        public static GameBuilder Game => new GameBuilder();

        public static Board Board(string boardSketch)
        {
            return BoardFabric.CreateBoard(boardSketch);
        }
    }
}