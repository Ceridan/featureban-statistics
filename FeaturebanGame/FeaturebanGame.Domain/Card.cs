namespace FeaturebanGame.Domain
{
    public class Card
    {
        public CardState State { get; }
        public Player Player { get; }

        public Card(Player player, CardState state = CardState.Available)
        {
            Player = player;
            State = state;
        }

        private bool Equals(Card other)
        {
            return State == other.State && Player.Equals(other.Player);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Card) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) State * 397) ^ Player.GetHashCode();
            }
        }

        public override string ToString()
        {
            var stateSymbol = State == CardState.Blocked ? 'B' : ' ';
            return $"[{Player.Name} {stateSymbol}]";
        }
    }
}