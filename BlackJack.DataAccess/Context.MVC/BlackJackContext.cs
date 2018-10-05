using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BlackJack.Entities.Enums;
using BlackJack.Entities;

namespace BlackJack.DataAccess.Context.MVC
{
    public class BlackJackContext : DbContext
    {
        static BlackJackContext()
        {
            Database.SetInitializer<BlackJackContext>(new ContextInitializer());
        }

        public BlackJackContext(string connectionString) : base(connectionString)
        { }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerCard> PlayersCards { get; set; }
        public DbSet<ExceptionDetail> ExceptionDetails { get; set; }
    }

    public class ContextInitializer : CreateDatabaseIfNotExists<BlackJackContext>
    {
        protected override void Seed(BlackJackContext context)
        {
            var _cards = new List<Card>();
            int minCardValue;
            for (int i = 0; i < Enum.GetNames(typeof(Suit)).Length; i++)
            {
                minCardValue = 2;
                for (int j = 0; j < Enum.GetNames(typeof(Rank)).Length; j++)
                {
                    var card = new Card();
                    card.Rank = (Rank)j;
                    card.Suit = (Suit)i;
                    _cards.Add(card);

                    if (j < (int)Rank.Ten)
                    {
                        _cards.Last().Value = minCardValue++;
                    }
                    if (j == (int)Rank.Ace)
                    {
                        _cards.Last().Value = ++minCardValue;
                    }
                    if (j >= (int)Rank.Ten && j != (int)Rank.Ace)
                    {
                        _cards.Last().Value = minCardValue;
                    }

                    context.Cards.Add(card);
                }
            }
            context.SaveChanges();
        }
    }
}
