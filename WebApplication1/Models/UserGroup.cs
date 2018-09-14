using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    #region 群成员信息表
    public class UserGroupToUSer
    {
        [Key]
        public int? UG_id { get; set; }//自增ID
        public int? UG_UserID { get; set; }//用户ID
        public int? UG_GroupID { get; set; }//群ID
        public DateTime? UG_CreateTime { get; set; }//创建时间
        public string UG_GroupNick { get; set; }//群内用户昵称

        public string status { get; set; }//群员状态{管理员(admin)，群主{forkmaster}，普通成员{normal}}
        public int banned { get; set; }//禁言
    }
    #endregion


    #region 群信息
    public class UserGroups
    {   [Key]
        public int? UG_id { get; set; }//群号
        [DisplayName("群名")]
        public string UG_kName { get; set; }//群名
        [DisplayName("公告")]
        public string UG_NOtice { get; set; }//公告
        [DisplayName("创建者")]

        public string UG_AdminID { get; set; }//创建者
        [DisplayName("创建时间")]
        public DateTime? UG_CreateTime { get; set; }//创建时间
        [DisplayName("群简介")]
        public string UG_Introd { get; set; }//群简介
        [DisplayName("头像")]
        public string UG_Icon { get; set; }//群头像




    }
    #endregion

 #region
    public class UserGroupsMsg
    {   [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int?MS_ID { get; set; }//消息ID数
        public int? GR_ID { get; set; }//群号
        public string GR_Content { get; set; }//消息内容
        public int? GR_FromID { get; set; }//发送者ID
        public string GR_FromName { get; set; }//发送者姓名
        public DateTime? GR_CreateTime { get; set; }//发送时间
    
    
    }

#endregion
}