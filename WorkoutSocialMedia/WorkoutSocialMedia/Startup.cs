using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkoutSocialMedia.Startup))]
namespace WorkoutSocialMedia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
