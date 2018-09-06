namespace FeatureGame.Domain.Tests.DSL
{
    public static class Create
    {
        public static PlayerBuilder Player => new PlayerBuilder();
        public static GameBuilder Game => new GameBuilder();
    }
}