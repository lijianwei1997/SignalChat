using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Filter;


namespace WebApplication1.Controllers
{
    [BasicAuthAttribute]
    public class ChatController : Controller
    {
        private static readonly context db = new context();
        // GET: Chat
        public ActionResult Index()
        {
            return View("Chatdemo");
        }

        public string ChatUI(int msgid)
        {
            string str = "";
             var id = Convert.ToInt32(Request.Params["groupid"].ToString().Trim());
            //foreach (var i in UserMsg(id))
            //{
                 str += Common.Common.getJsonByObject(UserMsg(msgid,id));
            //}
         

            return str;
            

        }
    
        public JsonResult UploadImage(HttpPostedFileBase filebase)
        {
           
            HttpPostedFileBase file = Request.Files["image"];
          
             string filename;
             string FileName;
             string savePath;
                 filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
                string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
                int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                string FileType = "jpg,jpeg,png";//定义上传文件的类型字符串

                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                string path = AppDomain.CurrentDomain.BaseDirectory + "Pictures/";
                savePath = Path.Combine(path, FileName);
                file.SaveAs(savePath);

                return Json("../../Pictures/" + FileName);
        }//图片上传

        public ActionResult Chat(int id)
        {
            ViewBag.groupid = id;
            ViewBag.GroupName = Common.Common.GetGroupName(id);
            List<UserGroupToUSer> us = db.UserGroupToUsers.Where(a => a.UG_GroupID == id).ToList();
            ViewData["Users"] = us;
           
            return View("Chatdemo");
        }//聊天主界面
        
        

        public  List<UserGroupsMsg> UserMsg(int id,int groupid)
        {
            List<UserGroupsMsg> msg = new List<UserGroupsMsg>();
            if (id == 0)
            {
                msg= db.UserGroupMsgs.Where(a=>a.GR_ID==groupid).OrderByDescending(a => a.MS_ID).ToList();
                return msg.Take<UserGroupsMsg>(20).ToList();
            }
            else
            {
                msg = db.UserGroupMsgs.Where(a => a.MS_ID < id).Where(a => a.GR_ID == groupid).OrderByDescending(a => a.MS_ID).ToList();
                return msg.Take<UserGroupsMsg>(20).ToList();
            }
        
        
        }//聊天信息表

        public ActionResult AddGroup()
        {

            return View();
        }//添加群

        public ActionResult AddUserToG(int? id)
        {
            ViewBag.groupid = id;
            return View("AddUserToGroup");
        }//添加用户到群视图

        public ActionResult AddUsers(int? id)
        {
            ViewBag.groupid = id;
            return View("ADUG");
        }//添加用户到群视图
        public string AddGroupUsers(int[] ids)//添加群用户
        {
            List<UserInfo> us = new List<UserInfo>();
            List<UserGroupToUSer> ut = new List<UserGroupToUSer>();
            var id = Convert.ToInt32(Request.Params["id"].ToString().Trim());
            foreach(var item in ids)
            {

                UserInfo ls = db.UserInfos.Where(a => a.userid== item).FirstOrDefault();
                if (ls != null)
                {

                    us.Add(ls);
                }
                else
                { 
                }
            }
        
            foreach (var item in us)
            {
                UserGroupToUSer iu = new UserGroupToUSer();
                iu.UG_CreateTime = DateTime.Now;
                iu.UG_GroupID = id;
                iu.UG_GroupNick = "";
                iu.UG_id = 1;
                iu.UG_UserID = item.userid;
                iu.status = "normal";
                iu.banned = 0;
                ut.Add(iu);
            
            }
            db.UserGroupToUsers.AddRange(ut);
            if (db.SaveChanges() > 0)
            {
                return "用户添加成功!";

            }
            else
            {
                return "用户添加失败！";
            }

           
        }

        public ActionResult Main()
        {

            return View();
        }//返回主页面
    }
}