using System.Collections.Generic;

namespace FeaturebanGame.Domain
{
    public class Game
    {
        public uint Turn { get; set; }
        private List<Player> _players;

        public void NextTurn()
        {
            foreach (var player in _players)
            {
                player.Work(Coin.Drop());
            }
            
            // TODO: Game turn logic
        }
    }
}