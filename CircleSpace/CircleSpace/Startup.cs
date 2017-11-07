using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CircleSpace.Startup))]
namespace CircleSpace
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
