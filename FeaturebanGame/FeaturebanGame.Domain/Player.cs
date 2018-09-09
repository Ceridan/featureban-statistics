namespace FeaturebanGame.Domain
{
    public struct Player
    {
        public string Name { get; }

        public Player(string name)
        {
            Name = name ?? string.Empty;
        }

        public CoinFlipResult FlipTheCoin(ICoin coin)
        {
            return coin.Flip();
        }

        public static bool operator ==(Player player1, Player player2)
        {
            return player1.Name == player2.Name;
        }

        public static bool operator !=(Player player1, Player player2)
        {
            return !(player1 == player2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Player))
                return false;

            var player = (Player) obj;
            return Name == player.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}