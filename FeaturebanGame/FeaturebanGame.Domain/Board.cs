using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeaturebanGame.Domain
{
    public struct Board
    {
        private readonly int _limit;

        public BacklogColumn BackLog { get; }
        public WipColumn Dev { get; }
        public WipColumn Test { get; }
        public DoneColumn Done { get; }

        public Board(int limit)
        {
            _limit = limit;
            BackLog  = new BacklogColumn();
            Dev = new WipColumn(name: "Dev", limit: limit);
            Test = new WipColumn(name: "Test", limit: limit);
            Done = new DoneColumn();
        }

        public List<Card> GetOrderedPlayerCards(Player player)
        {
            var cards = new List<Card>();
            cards.AddRange(Test.Cards.Where(x => x.Player == player));
            cards.AddRange(Dev.Cards.Where(x => x.Player == player));
            return cards;
        }

        public List<Card> GetOrderedCards()
        {
            var cards = new List<Card>();
            cards.AddRange(Test.Cards);
            cards.AddRange(Dev.Cards);
            return cards;
        }

        public void BlockCard(Card card)
        {
            ChangeCardState(card, CardState.Blocked);
        }

        public void UnblockCard(Card card)
        {
            ChangeCardState(card, CardState.Available);
        }

        public bool TryCreateNewCardFor(Player player)
        {
            var card = BackLog.GenerateNewCardForPlayer(player);
            return Dev.AddCard(card);
        }

        public bool TryMoveCard(Card card)
        {
            if (card.State == CardState.Blocked)
                return false;

            if (Test.Cards.Contains(card))
            {
                Test.RemoveCard(card);
                Done.CardCount++;
                return true;
            }

            if (Dev.Cards.Contains(card))
            {
                if (Test.AddCard(card))
                {
                    Dev.RemoveCard(card);
                    return true;
                }

                return false;
            }

            throw new NullReferenceException("Card must be attached to Dev or Test column");
        }

        public override string ToString()
        {
            var height = Math.Max(Dev.Cards.Count, Test.Cards.Count);

            var sb = new StringBuilder();
            sb.AppendLine(
                _limit > 0
                    ? "| Backlog |  Dev (3) | Test (3) | Done |"
                    : "| Backlog |   Dev    |   Test   | Done |"
            );

            for (var i = 0; i < height; i++)
            {
                var devCard = Dev.Cards.Count > i
                    ? Dev.Cards[i].ToString()
                    : "      ";

                var testCard = Test.Cards.Count > i
                    ? Test.Cards[i].ToString()
                    : "      ";

                var doneCount = i == 0
                    ? $"{Done.CardCount}".PadLeft(4, ' ')
                    : "    ";

                sb.AppendLine($"|         |  {devCard}  |  {testCard}  | {doneCount} |");
            }

            return sb.ToString();
        }

        private void ChangeCardState(Card card, CardState newState)
        {
            if (card.State == newState)
                return;

            var wip = GetCardColumn(card);

            if (wip == null)
                throw new NullReferenceException("Card must be attached to Dev or Test column");

            var newCard = CardFabric.CreateCard(card.Player, newState);
            wip.ReplaceCard(card, newCard);
        }

        private WipColumn GetCardColumn(Card card)
        {
            if (Dev.Cards.Contains(card))
                return Dev;

            if (Test.Cards.Contains(card))
                return Test;

            return null;
        }
    }
}