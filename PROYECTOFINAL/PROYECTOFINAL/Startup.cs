using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PROYECTOFINAL.Startup))]
namespace PROYECTOFINAL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
