using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Plumbery.UI.MVC.Startup))]
namespace Plumbery.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
