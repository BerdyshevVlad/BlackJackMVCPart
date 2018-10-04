using System;
using System.Data.Entity;
using BlackJack.DataAccess.Enums;
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
            Card card = null;
            Array enumValuesList = Enum.GetValues(typeof(Rank));

            int cardMinValue = 2;
            int cardMaxValue = 14;
            int rankMinValue = 0;
            int rankMaxValue = 14;
            int enumJackValue = 11;
            int enumKingValue = 13;
            int JackQueenKingValues = 10;
            int AceValue = 11;
            int enumAceVlue = 14;

            try
            {
                foreach (var suit in Enum.GetNames(typeof(Suit)))
                {
                    for (int value = cardMinValue, rankValue = rankMinValue;
                        value <= cardMaxValue && rankValue <= rankMaxValue;
                        value++, rankValue++)
                    {
                        if (value >= enumJackValue && value <= enumKingValue)
                        {
                            card = new Card
                            {
                                Value = JackQueenKingValues,
                                Suit = suit,
                                Rank = enumValuesList.GetValue(rankValue).ToString()
                            };
                        }

                        if (value == enumAceVlue)
                        {
                            card = new Card
                            { Value = AceValue, Suit = suit, Rank = enumValuesList.GetValue(rankValue).ToString() };
                        }

                        if (value < enumJackValue)
                        {
                            card = new Card
                            { Value = value, Suit = suit, Rank = enumValuesList.GetValue(rankValue).ToString() };
                        }

                        context.Cards.Add(card);
                    }
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
