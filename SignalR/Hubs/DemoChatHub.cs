using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalR
{
  [HubName("DemoChatHub")]
  public class DemoChatHub : Hub
  {
    public void NewChatMessage(string name, string message)
    {
      Clients.All.sendMessage(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),name, message);
    }
    public override Task OnConnected()
    {     
      Clients.All.sendMessage(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),"系統管理員", "有人連進來了喔");
      return base.OnConnected();
    }
    //public void Hello()
    //{
    //  Clients.All.hello();
    //}
  }
}