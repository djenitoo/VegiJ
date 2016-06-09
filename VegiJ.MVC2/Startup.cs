using Microsoft.Owin;
using Owin;
using System.Web.Services.Description;

[assembly: OwinStartupAttribute(typeof(VegiJ.MVC2.Startup))]
namespace VegiJ.MVC2
{
    using Models;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        //public void ConfigureServices(ServiceCollection services)
        //{
        //    services.AddDbContext<ApplicationDbContext>(options =>
        //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        //}
    }
}
