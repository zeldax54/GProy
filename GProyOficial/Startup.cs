using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GProyOficial.Startup))]
namespace GProyOficial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
