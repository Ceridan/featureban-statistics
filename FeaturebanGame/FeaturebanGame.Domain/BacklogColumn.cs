namespace FeaturebanGame.Domain
{
    public class BacklogColumn
    {
        public Card GenerateNewCardForPlayer(Player player)
        {
            return CardFabric.CreateCard(player, CardState.Available);
        }
    }
}