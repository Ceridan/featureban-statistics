using System.Collections.Generic;
using System.Linq;
using FeaturebanGame.Domain;

namespace FeatureGame.Domain.Tests.DSL
{
    public class PlayerBuilder
    {
        private int _id = 1;
        private Board _board;
        private List<Card> _cards = new List<Card>();

        public PlayerBuilder WithBoard(Board board)
        {
            _board = board;
            return this;
        }

        public PlayerBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public Player Please()
        {
            var player = new Player(id: _id, name: null, board: _board);;
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

        public PlayerBuilder AssignAllAvailableCardsOnBoardToPlayer()
        {
            _cards = _board.GetOrderedCards()
                .Where(x => x.State == CardState.Available)
                .ToList();
            return this;
        }

        public PlayerBuilder AssignAllBlockedCardsOnBoardToPlayer()
        {
            _cards = _board.GetOrderedCards()
                .Where(x => x.State == CardState.Blocked)
                .ToList();
            return this;
        }
    }
}