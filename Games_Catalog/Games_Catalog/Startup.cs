using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Games_Catalog.Startup))]
namespace Games_Catalog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
