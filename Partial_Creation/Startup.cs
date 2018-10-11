using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Partial_Creation.Startup))]
namespace Partial_Creation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
