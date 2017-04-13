using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SlavStore.Startup))]
namespace SlavStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
