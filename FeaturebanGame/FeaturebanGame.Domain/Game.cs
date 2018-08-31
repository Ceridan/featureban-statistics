using System.Collections.Generic;

namespace FeaturebanGame.Domain
{
    public class Game
    {
        private readonly int _turnCount;
        private readonly Board _board;
        private readonly ICoin _coin;

        private List<Player> _players = new List<Player>();

        public Game(ICoin coin, int playerCount, int wipLimit, int turnCount)
        {
            _turnCount = turnCount;
            _board = new Board(wipLimit);
            _coin = coin;

            for (var i = 0; i < playerCount; i++)
            {
                _players.Add(new Player(_board) { Id = i + 1 });
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