using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalR
{
  public class ServerHub : Hub
  {
    public void SendMsg(string message)
    {
      //调用所有客户端的sendMessage方法
      Clients.All.sendMessage(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message);

    }

    public override Task OnConnected()
    {
      System.Diagnostics.Trace.WriteLine("客户端连接成功！");
      Clients.All.sendMessage(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "有人連進來了喔");
      return base.OnConnected();
    }

    //public void Hello()
    //{
    //  Clients.All.hello();
    //}
  }
}