using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YokyDoky.Startup))]
namespace YokyDoky
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
