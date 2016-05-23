using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SoftBottin.Startup))]
namespace SoftBottin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
