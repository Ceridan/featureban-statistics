using System.Collections.Generic;
using FeaturebanGame.Domain;

namespace FeatureGame.Domain.Tests.DSL
{
    public class PlayerBuilder
    {
        private Board _board;
        private List<Card> _cards = new List<Card>();

        public PlayerBuilder WithBoard(Board board)
        {
            _board = board;
            return this;
        }

        public Player Please()
        {
            var player = new Player(_board);
            foreach (var card in _cards)
            {
                card.Player = player;
            }
            
            return player;
        }

        public PlayerBuilder AssignAllCardsOnBoardToPlayer()
        {
            _cards = _board.GetOrderedCards();
            return this;
        }
    }
}