using Newtonsoft.Json.Serialization;
using Pizzeria.Service.Filters;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Pizzeria.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Cors
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API configuration and services
            config.Filters.Add(new AppExceptionLogger());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}
