using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FirstASPnetApplication.Startup))]
namespace FirstASPnetApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
