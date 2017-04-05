using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iassetTechnicalTest.Startup))]
namespace iassetTechnicalTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
