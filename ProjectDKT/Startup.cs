using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectDKT.Startup))]
namespace ProjectDKT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
