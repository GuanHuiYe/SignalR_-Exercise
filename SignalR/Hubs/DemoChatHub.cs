using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace SignalR
{
  [HubName("DemoChatHub")]
  public class DemoChatHub : Hub
  {

    public class User
    {
      [Key]
      public string ConnectionID { get; set; }//連線的ID
      public string Name { get; set; }//使用者名稱

      //建構子方法
      public User(string name, string connectionId)
      {
        this.Name = name;
        this.ConnectionID = connectionId;
      }
    }

    public static List<User> userList = new List<User>();

  
    public override Task OnConnected()
    {
      Clients.All.sendMessage(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "系統管理員", "有人連進來了喔");

      //看看有沒有這個使用者
      var userConnectionID = userList.Where(w => w.ConnectionID == Context.ConnectionId).SingleOrDefault();
           
      //沒有的話，就增加
      if (userConnectionID == null)
      {
        //使用user建構子把 connectionId放入      
        userList.Add(new User("", Context.ConnectionId));
      }
      return base.OnConnected();
    }

    public void UpdateUser(string name)
    {
      //查询用户
      var user = userList.SingleOrDefault(u => u.ConnectionID == Context.ConnectionId);
      if (user != null)
      {
        user.Name = name;
        Clients.Client(Context.ConnectionId).showId(name,Context.ConnectionId);
      }
      GetOnlineUsers();
    }


    //上線列表
    public void GetOnlineUsers()
    {
      var list = userList.Select(s => new { s.Name, s.ConnectionID }).ToList();
      string jsonList = JsonConvert.SerializeObject(list);
      Clients.All.GetOnlineUsers(jsonList);
    }


    //給全部人發訊息
    public void SendAllMessage(string name, string message)
    {
      //Clients 往前端打事件  All 全部人  sendMessage 事件名稱
      Clients.All.sendAllMessage(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), name, message);
    }


    //給指定的人發送訊息
    public void SendUserMessage(string connectionId,string name, string message)
    {
      //Clients.All.hello();
      //找有沒有這個人
      var user = userList.Where(s => s.ConnectionID == connectionId).FirstOrDefault();
      if (user != null)
      {
        //給自己發訊息
        Clients.Client(Context.ConnectionId).SendUserMessage(connectionId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), name , message);
        
        //給對方發訊息
        Clients.Client(connectionId).SendUserMessage(Context.ConnectionId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") ,name , message);
       
      }
      else
      {
        Clients.Client(Context.ConnectionId).showMessage("該使用者已離線");
      }
    }




    //離線
    public override Task OnDisconnected(bool stopCalled)
    {
      var user = userList.Where(p => p.ConnectionID == Context.ConnectionId).FirstOrDefault();
      //是否有這個用戶，有的刪除
      if (user != null)
      {
        //刪除使用者
        userList.Remove(user);
      }
      GetOnlineUsers();//更新上線使用者
      return base.OnDisconnected(stopCalled);
    }
  }
}