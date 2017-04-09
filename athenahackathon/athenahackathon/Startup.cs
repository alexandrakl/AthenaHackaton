using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(athenahackathon.Startup))]
namespace athenahackathon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
