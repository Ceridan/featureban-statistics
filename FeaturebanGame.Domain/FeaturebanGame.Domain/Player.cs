namespace FeaturebanGame.Domain
{
    public class Player
    {
        private readonly Board _board;

        public int Id { get; set; }
        public string Name { get; set; }

        public Player(Board board)
        {
            _board = board;
        }
    }
}