using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LMS_MVC.Startup))]
namespace LMS_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
