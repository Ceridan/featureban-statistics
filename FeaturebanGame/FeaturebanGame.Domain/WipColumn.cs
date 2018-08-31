using System.Collections.Generic;

namespace FeaturebanGame.Domain
{
    public class WipColumn
    {
        private readonly List<Card> _cards = new List<Card>();

        public WipColumn(int limit)
        {
            Limit = limit;
        }

        public int Number { get; set; }
        public int Limit { get; }

        public IReadOnlyList<Card> Cards => _cards.AsReadOnly();

        public bool AddCard(Card card)
        {
            if (_cards.Count < Limit || Limit == 0)
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
    }
}