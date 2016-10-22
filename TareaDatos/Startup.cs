using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TareaDatos.Startup))]
namespace TareaDatos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
