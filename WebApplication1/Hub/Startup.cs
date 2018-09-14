using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(WebApplication1.Hub.Startup))]

namespace WebApplication1.Hub
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {

            var config = new HubConfiguration
            {
                 EnableJavaScriptProxies= true,
                
            };
            app.MapSignalR(config);
        }
    }
}
