namespace FeaturebanGame.Domain
{
    public struct Card
    {
        public int Id { get; }
        public CardState State { get; }
        public Player Player { get; }

        public Card(int id, Player player, CardState state = CardState.Available)
        {
            Id = id;
            Player = player;
            State = state;
        }

        public static bool operator ==(Card card1, Card card2)
        {
            return card1.Id == card2.Id;
        }

        public static bool operator !=(Card card1, Card card2)
        {
            return !(card1 == card2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Card))
                return false;

            var card = (Card) obj;
            return Id == card.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            var stateSymbol = State == CardState.Blocked ? 'B' : ' ';
            return $"[{Player.Name} {stateSymbol}]";
        }
    }
}