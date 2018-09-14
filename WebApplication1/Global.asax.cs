using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using Microsoft.Owin;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
       

        protected void Application_Start()
        {
            Database.SetInitializer<context>(new CreateDatabaseIfNotExists<context>());
            Database.SetInitializer<context>(new DropCreateDatabaseIfModelChanges<context>());
            
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }


       
       
    }
}
