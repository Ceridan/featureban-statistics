namespace FeaturebanGame.Domain
{
    public class Game
    {
        private readonly Coin _coin = new Coin();

        public uint Turn { get; set; }

        public void NextTurn()
        {
            // TODO: Game turn logic
        }
    }
}