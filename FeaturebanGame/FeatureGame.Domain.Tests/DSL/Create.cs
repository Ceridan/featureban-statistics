using FeaturebanGame.Domain;

namespace FeatureGame.Domain.Tests.DSL
{
    public static class Create
    {
        public static BoardBuilder Board => new BoardBuilder();
        public static PlayerBuilder Player => new PlayerBuilder();
        public static WipColumnBuilder WipColumn => new WipColumnBuilder();
        public static GameBuilder Game => new GameBuilder();
    }

    public class GameBuilder
    {
        private int _turnCount;
        private int _limit;
        private int _playerCount;
        private ICoin _coin;


        public GameBuilder WithPlayers(int playerCount)
        {
            _playerCount = playerCount;
            return this;
        }


        public GameBuilder WithTurns(int turnCount)
        {
            _turnCount = turnCount;
            return this;
        }

        public GameBuilder WithCoin(ICoin coin)
        {
            _coin = coin;
            return this;
        }

        public Game Please()
        {
            return new Game(
                playerCount: _playerCount,
                wipLimit: _limit,
                turnCount: _turnCount,
                coin: _coin ?? new Coin());
        }
    }
}