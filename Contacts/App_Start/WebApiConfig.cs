using Contacts.App_Start;
using System.Web.Http;
using Contacts.Repositories;
using Contacts.Repositories.Interfaces;
using Unity;
using Unity.Lifetime;

namespace Contacts
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
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = RouteParameter.Optional, id = RouteParameter.Optional }
            );

            config.Formatters.Add(new BrowserJsonFormatter());

            // Dependency Injecor Resolvers
            var container = new UnityContainer();

            container.RegisterType<IContactRepository, ContactRepository>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
