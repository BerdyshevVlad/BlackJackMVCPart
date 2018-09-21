using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Services;

namespace BlackJack.BusinessLogic
{
    public static class AutofacConfig
    {
        public static void Configure(ContainerBuilder builder, string connectionString)
        {
            builder.RegisterType<GameService>().As<IGameService>();
            DataAccess.AutofacConfig.Configure(builder, connectionString);
        }
    }
}
