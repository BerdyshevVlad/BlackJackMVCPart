using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BlackJack.DataAccess.Context.MVC;
using BlackJack.DataAccess.Interfaces;
using BlackJack.DataAccess.Repositories;

namespace BlackJack.DataAccess
{
    public static class AutofacConfig
    {
        public static void Configure(ContainerBuilder builder, string connectionString)
        {
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).WithParameter("connectionString", connectionString);
            builder.RegisterType<CardRepository>().As<ICardRepository>().WithParameter("connectionString", connectionString);
            builder.RegisterType<PlayerCardRepository>().As<IPlayerCardRepository>().WithParameter("connectionString", connectionString);
            builder.RegisterType<PlayerRepository>().As<IPlayerRepository>().WithParameter("connectionString", connectionString);
            builder.RegisterType<BlackJackContext>().As<BlackJackContext>().WithParameter("connectionString", connectionString);
        }
    }
}
