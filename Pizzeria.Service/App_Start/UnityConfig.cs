using Pizzeria.BL;
using Pizzeria.Repositories;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace Pizzeria.Service
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IOrderBL, OrderBL>();
            container.RegisterType<IHomeBL, HomeBL>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}