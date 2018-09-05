using System.Collections.Generic;
using System.Linq;
using FeaturebanGame.Domain;

namespace FeatureGame.Domain.Tests.DSL
{
    public class PlayerBuilder
    {
        private string _name = "AB";
        private Board _board;
        private ICoin _coin;
        private List<Card> _cards = new List<Card>();

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
            var player = new Player(_name, _board, _coin ?? new Coin());
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