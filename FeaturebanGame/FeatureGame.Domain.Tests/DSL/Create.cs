namespace FeatureGame.Domain.Tests.DSL
{
    public static class Create
    {
        public static BoardBuilder Board => new BoardBuilder();
        public static PlayerBuilder Player => new PlayerBuilder();
        public static WipColumnBuilder WipColumn => new WipColumnBuilder();
    }
}