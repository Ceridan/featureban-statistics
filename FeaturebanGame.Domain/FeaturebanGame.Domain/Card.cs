namespace FeaturebanGame.Domain
{
    public class Card
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public CardState State { get; set; }
        public Player Player { get; set; }
    }
}