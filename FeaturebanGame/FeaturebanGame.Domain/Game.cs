using System.Collections.Generic;

namespace FeaturebanGame.Domain
{
    public struct Game
    {
        private readonly int _turnCount;
        private readonly Board _board;
        private readonly ICoin _coin;
        private readonly List<Player> _players;

        public Game(ICoin coin, int playerCount, int wipLimit, int turnCount)
        {
            _turnCount = turnCount;
            _board = new Board(wipLimit);
            _coin = coin;
            _players = new List<Player>();

            for (var i = 0; i < playerCount; i++)
            {
                _players.Add(new Player(id: i + 1, name: null, board: _board));
            }
        }

        public int Play()
        {
            for (var i = 0; i < _turnCount; i++)
            {
                NextTurn();
            }

            return _board.DoneColumn.CardCount;
        }

        private void NextTurn()
        {
            foreach (var player in _players)
            {
                player.DoWork(_coin.Drop());
            }
        }
    }
}