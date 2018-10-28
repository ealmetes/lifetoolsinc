using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lifetoolsinc.Startup))]
namespace lifetoolsinc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
