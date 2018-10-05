using BlackJack.Entities.Enums;

namespace BlackJack.Entities
{
    public class Card : BaseEntity
    {
        public int Value { get; set; }
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
    }
}
