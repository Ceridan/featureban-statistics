namespace FeaturebanGame.Domain
{
    public class Player
    {
        private readonly Board _board;

        public int Id { get; set; }
        public string Name { get; set; }

        public Player(Board board)
        {
            _board = board;
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
            
            var card = cards.FirstOrDeafult(x => x.State == CardState.Available);
            if (card != null)
            {
                _board.BlockCard(card);
            }
            TakeNewCard();
        }

        private bool TakeNewCard()
        {
            bool result = _board.GetNewCardFor(this);
            if (result) return true;
            return false;
        }

        private bool WorkWithCards(object cards)
        {
            var availableCards = cards.Where(x => x.State == CardState.Available);
            foreach (var card in availableCards)
            {
                var result = _board.MoveCard(card);
                if (result) return true;
            }

            var blockedCard = cards.FirstOrDeafult(x => x.State == CardState.Blocked);
            if (blockedCard != null)
            {
                _board.UnblockCard(blockedCard);
                return true;
            }

            return false;
        }
    }
}