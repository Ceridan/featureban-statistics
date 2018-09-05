using System.Collections.Generic;
using System.Linq;

namespace FeaturebanGame.Domain
{
    public struct Board
    {
        private readonly BacklogColumn _backlog;
        private readonly List<WipColumn> _wips;
        private readonly DoneColumn _done;

        public IReadOnlyList<WipColumn> Wips => _wips.AsReadOnly();
        public DoneColumn DoneColumn => _done;

        public Board(int limit)
        {
            _backlog  = new BacklogColumn();
            _wips = new List<WipColumn>
            {
                new WipColumn(limit) { Number = 1 },
                new WipColumn(limit) { Number = 2 },
            };
            _done = new DoneColumn();
        }

        public List<Card> GetOrderedPlayerCards(Player player)
        {
            var cards = new List<Card>();
            foreach (var wipColumn in _wips.OrderByDescending(x => x.Number))
            {
                cards.AddRange(wipColumn.Cards.Where(x => x.Player == player));
            }

            return cards;
        }

        public List<Card> GetOrderedCards()
        {
            var cards = new List<Card>();
            foreach (var wipColumn in _wips.OrderByDescending(x => x.Number))
            {
                cards.AddRange(wipColumn.Cards);
            }

            return cards;
        }

        public void BlockCard(Card card)
        {
            card.State = CardState.Blocked;
        }

        public void UnblockCard(Card card)
        {
            card.State = CardState.Available;
        }

        public bool CreateNewCardFor(Player player)
        {
            var card = _backlog.GenerateNewCardForPlayer(player);
            return _wips.First().AddCard(card);
        }

        public bool MoveCard(Card card)
        {
            if (card.State == CardState.Blocked) return false;
            
            for (int i = 0; i < _wips.Count; i++)
            {
                if (_wips[i].Cards.Contains(card))
                {
                    if (i == _wips.Count - 1)
                    {
                        _wips[i].RemoveCard(card);
                        _done.CardCount++;
                        return true;
                    }

                    if (_wips[i + 1].AddCard(card))
                    {
                        _wips[i].RemoveCard(card);
                        return true;
                    }

                    return false;
                }
            }
            
            return false;
        }
    }
}