using AutoMapper;
using TrailGamingSite.Models.Model;
using System.Web.Http;

namespace TrailGamingSiteAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            Mapper.Initialize(oMap =>
            {
                oMap.CreateMap<Customer, CustomerItem>();
                oMap.CreateMap<Transaction, TransactionItem>();
            });

           // var builder = new ContainerBuilder();
           // builder.RegisterType<TrailSiteRepository>().As<ITrailSiteRepository>();
            //builder.RegisterType<TrailSiteRepository>().As<ITrailSiteRepository>();
            //builder.RegisterType<Car>().As<ICar>();
           // var container = builder.Build();

        }
    }
}
