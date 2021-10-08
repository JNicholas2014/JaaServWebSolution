using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JaaServWebSolution.Startup))]
namespace JaaServWebSolution
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
