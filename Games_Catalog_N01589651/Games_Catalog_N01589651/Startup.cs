using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Games_Catalog_N01589651.Startup))]
namespace Games_Catalog_N01589651
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
