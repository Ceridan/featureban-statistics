using System.Collections.Generic;
using System.Linq;
using FeaturebanGame.Domain;

namespace FeatureGame.Domain.Tests.DSL
{
    public class PlayerBuilder
    {
        private string _name = "AB";
        private Board _board;

        public PlayerBuilder WithBoard(Board board)
        {
            _board = board;
            return this;
        }

        public PlayerBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public Player Please()
        {
            var player = new Player(_name, _board);
            return player;
        }
    }
}