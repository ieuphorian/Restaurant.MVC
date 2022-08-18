using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Restaurant.WebApi.Startup))]
namespace Restaurant.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
