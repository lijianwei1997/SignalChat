using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebApplication1.Models;
using System.Data.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using WebApplication1.Hub;

namespace WebApplication1.Hub
{
    [HubName("chathub")]
    public class Chathub : Microsoft.AspNet.SignalR.Hub
    {
        public readonly context db = new context();

        private UserGroupsMsg ug = new UserGroupsMsg();
        public void Send(int name, string message,string groupid)
        {
            var Name = db.UserInfos.Where(a => a.userid == name).FirstOrDefault().name;
            //send message to all pages
            //Clients.All.SentMsgToPages(Name, message);
            //Groups.Add(Context.ConnectionId, groupid);
            Clients.Group(groupid,new string[0]).sendmessage(Name, message);
            SavaMessage(name,message,groupid);

           
        }
        public void JoinGroup(string groupname)
        {
            Groups.Add(Context.ConnectionId, groupname);
        }

        //保存信息

        public void SavaMessage(int name, string message,string groupid)
        {
            
            ug.GR_Content=message;
            ug.GR_FromID=Convert.ToInt32(name);
            ug.GR_FromName=db.UserInfos.Where(a=>a.userid==name).FirstOrDefault().name;
            ug.GR_CreateTime=DateTime.Now;
            ug.GR_ID =Convert.ToInt32(groupid.Trim());
            db.UserGroupMsgs.Add(ug);
            db.SaveChanges();
        }



        public void AddGroup(string id,string groupName)
        {
            Groups.Add(id,groupName);
        }

       
    }
}