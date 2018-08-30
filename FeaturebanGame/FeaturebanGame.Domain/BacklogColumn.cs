namespace FeaturebanGame.Domain
{
    public class BacklogColumn
    {
        private int _lastCardId = 1;

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