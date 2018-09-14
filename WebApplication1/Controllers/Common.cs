using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Text;
using WebApplication1.Models;

namespace Common
{
    public class Common
    {

        private static context db = new context();
        public static string GetSessionID()
        {
            return HttpContext.Current.Session.SessionID;
        }

        public static string getJsonByObject(Object obj)
        {
            //实例化DataContractJsonSerializer对象，需要待序列化的对象类型
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            //实例化一个内存流，用于存放序列化后的数据
            MemoryStream stream = new MemoryStream();
            //使用WriteObject序列化对象
            serializer.WriteObject(stream, obj);
            //写入内存流中
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            //通过UTF8格式转换为字符串
            return Encoding.UTF8.GetString(dataBytes);
        }

        public static UserLogin GetUser()
        {
            return HttpContext.Current.Session[GetSessionID()] as UserLogin;
        }
        //public static user GetUserDetail()
        //{

        //}
        public static  UserInfo GetTown(int uid)
        {
            return db.UserInfos.Where(a => a.userid == uid).FirstOrDefault();
        
        }


        public static List<UserInfo> GetAllUser(string town,string township)
        {

            return db.UserInfos.Where(a=>a.town==town).Where(a=>a.township==township).ToList();
        }
        public static List<UserGroupToUSer> GetGroupList(int id)
        {

            return db.UserGroupToUsers.Where(a => a.UG_UserID == id).Distinct().ToList();

        } //返回用户加入的群  


        public static List<UserGroupToUSer> GetGroupUserList(int id)
        {
            return db.UserGroupToUsers.Where(a => a.UG_GroupID == id).Distinct().ToList();
        }//返回群成员

        public static string GetGroupName(int id)
        {
            return db.UserGroups.Where(a => a.UG_id == id).FirstOrDefault().UG_kName;
        }

        public static string GetUserName(int id)
        {

            return db.UserInfos.Where(a => a.userid == id).FirstOrDefault().name;
        }

        public static bool CheckAdmin(int groupid)
        {
         return   db.UserGroups.Where(a => a.UG_id == groupid).FirstOrDefault().UG_AdminID == Common.GetUser().id.ToString().Trim()? true: false;
        }

        public static bool Check(int groupid,int userid)
        {
            try
            {
                int id = (int)db.UserGroupToUsers.Where(a => a.UG_GroupID == groupid & a.UG_UserID == userid).FirstOrDefault().UG_id;
                if (id!=0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch {
                return false;
            }
          
        }
    }
}