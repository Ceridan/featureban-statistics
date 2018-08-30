using System.Collections.Generic;
using System.Linq;

namespace FeaturebanGame.Domain
{
    public class Board
    {
        private readonly BacklogColumn _backlog = new BacklogColumn();
        private readonly List<WipColumn> _wips = new List<WipColumn>();
        private readonly DoneColumn _done = new DoneColumn();

        public IReadOnlyList<WipColumn> Wips => _wips.AsReadOnly();
        public DoneColumn DoneColumn => _done;

        public Board(int limit)
        {
            _wips.AddRange(new []
            {
                new WipColumn { Number = 1, Limit = limit },
                new WipColumn { Number = 2, Limit = limit },
            });
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

        public bool GetNewCardFor(Player player)
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

                    if (_wips[i + 1].Limit == 0 || _wips[i + 1].Limit > _wips[i + 1].Cards.Count)
                    {
                        _wips[i].RemoveCard(card);
                        _wips[i + 1].AddCard(card);
                        return true;
                    }

                    return false;
                }
            }
            
            return false;
        }
    }
}