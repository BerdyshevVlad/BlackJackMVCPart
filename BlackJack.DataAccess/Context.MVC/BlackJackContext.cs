﻿using System.Data.Entity;
using BlackJack.Entities;

namespace BlackJack.DataAccess.Context.MVC
{
    public class BlackJackContext : DbContext
    {
        static BlackJackContext()
        {
            Database.SetInitializer<BlackJackContext>(new ContextInitializer());
        }

        public BlackJackContext() : base("BlackJackContext")
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

        }
    }
}
