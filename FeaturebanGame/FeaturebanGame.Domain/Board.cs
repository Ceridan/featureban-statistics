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
            Dev = new WipColumn(limit);
            Test = new WipColumn(limit);
            Done = new DoneColumn();
        }

        public void MakeTurnFor(Player player, CoinFlipResult coin)
        {
            var playerCards = GetOrderedPlayerCards(player);

            if (coin == CoinFlipResult.Tail)
            {
                if (TryWorkWithCards(playerCards)) return;
                if (TryCreateNewCardFor(player)) return;

                var allCards = GetOrderedCards();
                TryWorkWithCards(allCards);
                return;
            }

            var card = playerCards.FirstOrDefault(x => x.State == CardState.Available);
            if (card != null)
            {
                BlockCard(card);
            }
            TryCreateNewCardFor(player);
        }

        public override string ToString()
        {
            var height = Math.Max(Math.Max(Dev.Cards.Count, Test.Cards.Count), 1);

            var sb = new StringBuilder();
            sb.AppendLine(
                _limit > 0
                    ? $"| Backlog |  Dev ({_limit}) | Test ({_limit}) | Done |"
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

        private List<Card> GetOrderedPlayerCards(Player player)
        {
            var cards = new List<Card>();
            cards.AddRange(Test.Cards.Where(x => x.Player == player));
            cards.AddRange(Dev.Cards.Where(x => x.Player == player));
            return cards;
        }

        private List<Card> GetOrderedCards()
        {
            var cards = new List<Card>();
            cards.AddRange(Test.Cards);
            cards.AddRange(Dev.Cards);
            return cards;
        }

        private bool TryWorkWithCards(List<Card> cards)
        {
            var availableCards = cards.Where(x => x.State == CardState.Available);
            foreach (var card in availableCards)
            {
                if (TryMoveCard(card))
                    return true;
            }

            var blockedCard = cards.FirstOrDefault(x => x.State == CardState.Blocked);
            if (blockedCard != null)
            {
                UnblockCard(blockedCard);
                return true;
            }

            return false;
        }

        private void BlockCard(Card card)
        {
            ChangeCardState(card, CardState.Blocked);
        }

        private void UnblockCard(Card card)
        {
            ChangeCardState(card, CardState.Available);
        }

        private bool TryCreateNewCardFor(Player player)
        {
            var card = BackLog.GenerateNewCardForPlayer(player);
            return Dev.AddCard(card);
        }

        private bool TryMoveCard(Card card)
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

        private void ChangeCardState(Card card, CardState newState)
        {
            if (card.State == newState)
                return;

            var wip = GetCardColumn(card);

            if (wip == null)
                throw new NullReferenceException("Card must be attached to Dev or Test column");

            var newCard = new Card(card.Player, newState);
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