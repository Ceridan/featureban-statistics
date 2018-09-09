namespace FeaturebanGame.Domain
{
    public class BacklogColumn
    {
        public Card GenerateNewCardForPlayer(Player player)
        {
            return new Card(player, CardState.Available);
        }
    }
}