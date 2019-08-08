using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(aa.Startup))]
namespace aa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
