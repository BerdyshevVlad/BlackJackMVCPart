namespace BlackJack.Entities
{
    public class Card : BaseEntity
    {
        public int Value { get; set; }
        public string Suit { get; set; }
        public string Rank { get; set; }
    }
}
