namespace FeaturebanGame.Domain
{
    public static class CardFabric
    {
        private static int _lastCardId = 1;

        public static Card CreateCard(Player player, CardState state = CardState.Available)
        {
            return new Card(
                id: _lastCardId++,
                player: player,
                state: state
            );
        }
    }
}