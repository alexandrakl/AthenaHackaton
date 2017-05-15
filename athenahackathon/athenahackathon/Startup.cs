using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyArkaProject.Startup))]
namespace MyArkaProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
