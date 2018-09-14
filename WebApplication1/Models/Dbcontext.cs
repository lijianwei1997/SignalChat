using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication1.Models;


public class context:DbContext
{

    public context():base("Dbcontext")
    {
    }

    
    public  DbSet<UserGroupsMsg> UserGroupMsgs { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }

    public DbSet<UserGroupToUSer> UserGroupToUsers { get; set; }

    public DbSet<UserGroups> UserGroups { get; set; }
    public DbSet<UserInfo> UserInfos { get; set; }


    }
