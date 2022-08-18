using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Restaurant.Data;
using Restaurant.Services.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Restaurant.WebApi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IContainer container;
        public static IContainer webApiContainer;
        protected void Application_Start()
        {
            RegisterServices();
            RegisterWebApiServices();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static void RegisterServices()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<AppDbContext>().As<AppDbContext>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterWebApiServices()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<AppDbContext>().As<AppDbContext>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            webApiContainer = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(webApiContainer);
        }
    }
}
