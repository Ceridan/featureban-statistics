using System.Collections.Generic;
using FeaturebanGame.Domain;

namespace FeatureGame.Domain.Tests.DSL
{
    public class GameBuilder
    {
        private int _turnCount;
        private int _limit;
        private IEnumerable<string> _playerNames;
        private ICoin _coin;


        public GameBuilder WithPlayers(IEnumerable<string> playerNames)
        {
            _playerNames = playerNames;
            return this;
        }

        public GameBuilder WithLimit(int limit)
        {
            _limit = limit;
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
                _playerNames,
                _turnCount,
                _limit,
                _coin ?? new Coin());
        }
    }
}