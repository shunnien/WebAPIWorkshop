using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CorsMVC.Startup))]
namespace CorsMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
