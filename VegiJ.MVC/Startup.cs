using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VegiJ.MVC.Startup))]
namespace VegiJ.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
