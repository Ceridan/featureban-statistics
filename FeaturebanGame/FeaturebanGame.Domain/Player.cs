using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace FeaturebanGame.Domain
{
    public struct Player
    {
        private readonly Board _board;
        private readonly ICoin _coin;

        public string Name { get;  }

        public Player(string name, Board board, ICoin coin)
        {
            Name = name ?? string.Empty;
            _board = board;
            _coin = coin;
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

        public CoinDropResult DropTheCoin()
        {
            return _coin.Drop();
        }

        public void Play(CoinDropResult coin)
        {
            var cards = _board.GetOrderedPlayerCards(this);
            if (coin == CoinDropResult.Tail)
            {
                if (WorkWithCards(cards)) return;
                if (PullNewCard()) return;

                var allCards = _board.GetOrderedCards();
                WorkWithCards(allCards);
                return;
            }
            
            var card = cards.FirstOrDefault(x => x.State == CardState.Available);
            if (card != null)
            {
                _board.BlockCard(card);
            }
            PullNewCard();
        }

        private bool PullNewCard()
        {
            bool result = _board.CreateNewCardFor(this);
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