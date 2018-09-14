using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using System.Web.Mvc;
using System.Threading;

namespace WebApplication1.Controllers
{
    public class UserGroupController : Controller
    {
        private context db = new context();

        public ActionResult AddGroup(FormCollection Form)
        {
            string id, Groupname, Groupintro, Groupnotice;
            Groupname=Request.Form["GroupName"].ToString();
            Groupintro = Request.Form["GroupIntro"].ToString();
            Groupnotice = Request.Form["GroupNotice"].ToString();
            id = Request.Form["uid"].ToString();
            //添加群表
            UserGroups ug = new UserGroups();
            ug.UG_CreateTime = DateTime.Now;
            ug.UG_Icon = "~/Images/head.jpg";
            ug.UG_Introd = Groupintro;
            ug.UG_kName = Groupname;
            ug.UG_NOtice = Groupnotice;
            ug.UG_AdminID = (db.UserLogins.Where(a => a.id.ToString().Trim()== id.ToString().Trim()).FirstOrDefault() as UserLogin).id.ToString();
            db.UserGroups.Add(ug);
            db.SaveChanges();
            Thread.Sleep(1000);
            int groupid = Convert.ToInt32(db.UserGroups.Where(a => a.UG_AdminID == ug.UG_AdminID).OrderByDescending(a=>a.UG_id).FirstOrDefault().UG_id);
          
            UserGroupToUSer use = new UserGroupToUSer();
            use.banned = 0;
            use.status = "admin";
            use.UG_CreateTime = DateTime.Now;
            use.UG_GroupID = groupid;
            use.UG_GroupNick = "群主";
            use.UG_UserID=Convert.ToInt32(ug.UG_AdminID);
            db.UserGroupToUsers.Add(use);
           var me=(HttpContext.Session[HttpContext.Session.SessionID] as UserLogin).id;
           
           if (db.SaveChanges()>0)
           {
               return RedirectToAction("AddUserToG/"+groupid, "Chat");

           }
           else

           {
               return JavaScript("alert('群建立失败！')");
           }
          
          
        }//创建群






        public ActionResult AddUserToGroup(List<UserGroupToUSer> user)
        {
            foreach (UserGroupToUSer us in user)
            {
                db.UserGroupToUsers.Add(us);
            
            }
            if (db.SaveChanges() > 0)
            {

                return View("添加成功");
            }
            else
            {
            
            return JavaScript("alert('群成员添加失败')");
            }




        }//批量添加用户


      


        public string bannered(int userid, int groupid,int id)
        {
            UserGroupToUSer us = db.UserGroupToUsers.Where(a => a.UG_UserID == userid).Where(a => a.UG_GroupID == groupid).FirstOrDefault();
            us.banned = id;
            if (db.SaveChanges() > 0)
            {
                return "XX已经被禁言";
            }
            else
            {
                return "解禁成功";
            }


        }//禁言


        public string Delete(int userid, int groupid)
        {

            UserGroupToUSer user = db.UserGroupToUsers.Where(a => a.UG_UserID == userid).Where(a => a.UG_GroupID == groupid).FirstOrDefault();
            //db.Entry<UserGroupToUSer>(user).State = System.Data.Entity.EntityState.Deleted;
            db.UserGroupToUsers.Remove(user);
            if (db.SaveChanges() > 0)
            {

                return "XX用户已经被删除！";
            }
            else
            {
                return "XX用户删除失败！";
            }

        }//删除


        public bool CheckBannerd(int userid, int groupid)
        {

            return Convert.ToBoolean((db.UserGroupToUsers.Where(a => a.UG_UserID == userid).Where(a => a.UG_GroupID == groupid).FirstOrDefault() as UserGroupToUSer).banned);
        }//检测用户是否被禁言

      
        public string CroupList(int userid)
        {
            string str="";
            foreach (var item in Common.Common.GetGroupList(userid))
            {
                str+= Common.Common.getJsonByObject(item);
            }

            return str;
        }//返回用户群Json数据

        public string CroupUserList(int groupid)
        {
            string str = "";
            foreach (var item in Common.Common.GetGroupUserList(groupid))
            {
                str += Common.Common.getJsonByObject(item);
            }

            return str;
        }//返回群用户JSon数据
      
        public string DeleteGroup()
        {

            int groupid =Convert.ToInt32(Request.Params["groupid"].ToString());
          UserGroups user=db.UserGroups.Where(a => a.UG_id == groupid).FirstOrDefault();

          Del(groupid);
            //db.Entry<UserGroupToUSer>(user).State = System.Data.Entity.EntityState.Deleted;
          db.UserGroups.Remove(user);

            if (db.SaveChanges() > 0)
            {

                return "删除群成功";
            }
            else
            {
                return "删除群失败";
            }

        }//删除

        public bool Del(int groupid)
        {
            List<UserGroupToUSer> uus = db.UserGroupToUsers.Where(a => a.UG_GroupID == groupid).ToList();
            db.UserGroupToUsers.RemoveRange(uus);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }

   
}