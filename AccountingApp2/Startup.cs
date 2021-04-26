using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AccountingApp2.Startup))]
namespace AccountingApp2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }
}
