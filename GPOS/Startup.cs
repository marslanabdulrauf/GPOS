using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GPOS.Startup))]
namespace GPOS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
