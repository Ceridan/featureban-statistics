using System.Collections.Generic;
using System.Linq;
using FeaturebanGame.Domain;

namespace FeatureGame.Domain.Tests.DSL
{
    public class BoardBuilder
    {
        private List<Card> _cardsFirst = new List<Card>();
        private List<Card> _cardsSecond = new List<Card>();
        private int _limit = 0;

        public Board Please()
        {
            var board = new Board(_limit);
            foreach (var card in _cardsFirst)
            {
                board.Wips.First().AddCard(card);
            }
            foreach (var card in _cardsSecond)
            {
                board.Wips.Last().AddCard(card);
            }
            
            return board;
        }

        public BoardBuilder WithBlockedCard()
        {
            var blockedCard = new Card
            {
                State = CardState.Blocked
            };
            _cardsFirst.Add(blockedCard);

            return this;
        }

        public BoardBuilder WithAvailableCard()
        {
            var blockedCard = new Card
            {
                State = CardState.Available
            };
            _cardsFirst.Add(blockedCard);

            return this;
        }

        public BoardBuilder WithAvailableCardOnSecondWipColumn()
        {
            var blockedCard = new Card
            {
                State = CardState.Available
            };
            _cardsSecond.Add(blockedCard);

            return this;
        }

        public BoardBuilder WithCardOnFirstWipColumn(Card card)
        {
            _cardsFirst.Add(card);
            return this;
        }

        public BoardBuilder WithCardOnSecondWipColumn(Card card)
        {
            _cardsSecond.Add(card);
            return this;
        }

        public BoardBuilder WithWipLimit(int limit)
        {
            _limit = limit;
            return this;
        }
    }
}