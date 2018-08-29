namespace FeaturebanGame.Domain
{
    public struct BacklogColumn
    {
        private static int _lastCardId = 1;

        public Card GenerateNewCardForPlayer(Player player)
        {
            return new Card
            {
                Id = _lastCardId++,
                State = CardState.Available,
                Player = player
            };
        }
    }
}