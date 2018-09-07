using System;
using System.Collections.Generic;

namespace FeaturebanGame.Domain
{
    public struct Game
    {
        private readonly int _turnsCount;
        private readonly Board _board;
        private readonly ICoin _coin;
        private readonly List<Player> _players;

        public Game(IEnumerable<string> playerNames, int turnsCount, int wipLimit, ICoin coin)
        {
            _turnsCount = turnsCount;
            _board = new Board(wipLimit);
            _coin = coin;
            _players = new List<Player>();

            foreach (var playerName in playerNames)
            {
                _players.Add(new Player(playerName, _board));
            }
        }

        public int Play()
        {
            for (var i = 0; i < _turnsCount; i++)
            {
                NextTurn();
            }

            return _board.Done.CardCount;
        }

        private void NextTurn()
        {
            foreach (var player in _players)
            {
                var coinFlipResult = player.FlipTheCoin(_coin);
                player.Play(coinFlipResult);
            }
        }
    }
}