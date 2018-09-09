using System.Collections.Generic;

namespace FeaturebanGame.Domain
{
    public class WipColumn
    {
        private readonly int _limit;
        private readonly List<Card> _cards = new List<Card>();

        public WipColumn(int limit)
        {
            _limit = limit;
        }

        public IReadOnlyList<Card> Cards => _cards.AsReadOnly();

        public bool AddCard(Card card)
        {
            if (_cards.Count < _limit || _limit == 0)
            {
                _cards.Add(card);
                return true;
            }

            return false;
        }

        public void RemoveCard(Card card)
        {
            _cards.Remove(card);
        }

        public void ReplaceCard(Card oldCard, Card newCard)
        {
            _cards.Remove(oldCard);
            _cards.Add(newCard);
        }
    }
}