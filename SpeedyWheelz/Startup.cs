using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpeedyWheelz.Startup))]
namespace SpeedyWheelz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
