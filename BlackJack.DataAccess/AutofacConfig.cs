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
            builder.RegisterType<BlackJackContext>().AsSelf().WithParameter("connectionString", connectionString);
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>));
            builder.RegisterType<CardRepository>().As<ICardRepository>();
            builder.RegisterType<PlayerCardRepository>().As<IPlayerCardRepository>();
            builder.RegisterType<PlayerRepository>().As<IPlayerRepository>();
        }
    }
}
