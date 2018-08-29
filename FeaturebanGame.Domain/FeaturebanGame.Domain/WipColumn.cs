using System.Collections.Generic;

namespace FeaturebanGame.Domain
{
    public class WipColumn
    {
        private readonly List<Card> _cards = new List<Card>();

        public int Number { get; set; }
        public int Limit { get; set; }

        public IReadOnlyList<Card> Cards => _cards.AsReadOnly();
    }
}