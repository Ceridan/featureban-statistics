using System.Collections.Generic;
using System.Linq;
using FeaturebanGame.Domain;

namespace FeatureGame.Domain.Tests.DSL
{
    public class BoardBuilder
    {
        private List<Card> _cards = new List<Card>();
        private int _limit = 0;

        public Board Please()
        {
            var board = new Board(_limit);
            foreach (var card in _cards)
            {
                board.Wips.First().AddCard(card);
            }
            
            return board;
        }

        public BoardBuilder WithBlockedCard()
        {
            var blockedCard = new Card
            {
                State = CardState.Blocked
            };
            _cards.Add(blockedCard);

            return this;
        }

        public BoardBuilder WithAvailableCard()
        {
            var blockedCard = new Card
            {
                State = CardState.Available
            };
            _cards.Add(blockedCard);

            return this;
        }
    }
}