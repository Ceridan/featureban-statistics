using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace FeaturebanGame.Domain
{
    public struct Player
    {
        private readonly Board _board;

        public string Name { get;  }

        public Player(string name, Board board)
        {
            Name = name ?? string.Empty;
            _board = board;
        }

        public CoinFlipResult FlipTheCoin(ICoin coin)
        {
            return coin.Flip();
        }

        public static bool operator ==(Player player1, Player player2)
        {
            return player1.Name == player2.Name;
        }

        public static bool operator !=(Player player1, Player player2)
        {
            return !(player1 == player2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Player))
                return false;

            var player = (Player) obj;
            return Name == player.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}