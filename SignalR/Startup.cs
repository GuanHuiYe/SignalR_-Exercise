using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(SignalR.Startup))]

namespace SignalR
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      // 如需如何設定應用程式的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkID=316888
      app.Map("/signalr", map =>
      {
        map.UseCors(CorsOptions.AllowAll); //跨域map.UseCors(CorsOptions.AllowAll); //跨域
        map.RunSignalR(new HubConfiguration { EnableJSONP = true });
      });
    }
  }
}
