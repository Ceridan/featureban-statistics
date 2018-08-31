using System.Collections.Generic;
using FeaturebanGame.Domain;

namespace FeatureGame.Domain.Tests.DSL
{
    public class WipColumnBuilder
    {
        private int _limit = 0;
        private List<Card> _cards = new List<Card>(); 
        
        public WipColumnBuilder WithLimit(int limit)
        {
            _limit = limit;
            return this;
        }

        public WipColumnBuilder WithCard()
        {
            var card = new Card();
            _cards.Add(card);
            return this;
        }

        public WipColumn Please()
        {
            var wip = new WipColumn(_limit);

            foreach (var card in _cards)
            {
                wip.AddCard(card);
            }

            return wip;
        }
    }
}