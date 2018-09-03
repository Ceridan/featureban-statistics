using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace FeaturebanGame.Domain
{
    public struct Player
    {
        private readonly Board _board;

        public int Id { get; }
        public string Name { get;  }

        public Player(int id, string name, Board board)
        {
            Id = id;
            Name = name ?? string.Empty;
            _board = board;
        }

        public static bool operator ==(Player player1, Player player2)
        {
            return player1.Id == player2.Id && player1.Name == player2.Name;
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
            return Id == player.Id && Name == player.Name;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + Id.GetHashCode();
                hash = hash * 23 + Name.GetHashCode();
                return hash;
            }
        }

        public void DoWork(CoinDropResult drop)
        {
            var cards = _board.GetOrderedPlayerCards(this);
            if (drop == CoinDropResult.Tail)
            {
                if (WorkWithCards(cards)) return;
                if (TakeNewCard()) return;

                var allCards = _board.GetOrderedCards();
                WorkWithCards(allCards);
                return;
            }
            
            var card = cards.FirstOrDefault(x => x.State == CardState.Available);
            if (card != null)
            {
                _board.BlockCard(card);
            }
            TakeNewCard();
        }

        private bool TakeNewCard()
        {
            bool result = _board.AddNewCardFor(this);
            if (result) return true;
            return false;
        }

        private bool WorkWithCards(List<Card> cards)
        {
            var availableCards = cards.Where(x => x.State == CardState.Available);
            foreach (var card in availableCards)
            {
                var result = _board.MoveCard(card);
                if (result) return true;
            }

            var blockedCard = cards.FirstOrDefault(x => x.State == CardState.Blocked);
            if (blockedCard != null)
            {
                _board.UnblockCard(blockedCard);
                return true;
            }

            return false;
        }
    }
}