using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMSECMultiplObj.Startup))]
namespace CMSECMultiplObj
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
