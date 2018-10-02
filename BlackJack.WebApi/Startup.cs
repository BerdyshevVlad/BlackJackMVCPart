//using System.Configuration;
//using System.Reflection;
//using System.Web.Http;
//using System.Web.Mvc;
//using Autofac;
//using Autofac.Integration.Mvc;
//using Autofac.Integration.WebApi;

//namespace BlackJack.WebApi
//{
//    public class Startup
//    {
//        public void Configurator(IAppBuilder app)
//        {
//            //AutofacConfig.Initialize(config);
//            //app.UseAutofacMiddleware(container);

//            AreaRegistration.RegisterAllAreas();
//            GlobalConfiguration.Configure(WebApiConfig.Register);
//            var builder = new ContainerBuilder();
//            builder.RegisterControllers(Assembly.GetExecutingAssembly());
//            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
//            string connectionString = ConfigurationManager.ConnectionStrings["BlackJackContext"].ConnectionString;
//            BlackJack.BusinessLogic.AutofacConfig.Configure(builder, connectionString);
//            var container = builder.Build();
//            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
//            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
//            //app.UseAutofacMiddleware(container);
//        }
//    }
//}