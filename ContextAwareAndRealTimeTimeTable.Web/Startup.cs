using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContextAwareAndRealTimeTimeTable.Web.Startup))]
namespace ContextAwareAndRealTimeTimeTable.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
