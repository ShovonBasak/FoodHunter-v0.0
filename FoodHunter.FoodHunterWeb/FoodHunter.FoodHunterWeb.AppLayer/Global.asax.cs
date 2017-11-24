using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FoodHunter.Web;
using FoodHunter.Web.DataLayer;

namespace FoodHunter.Web.AppLayer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);


        }

        protected void Session_Start()
        {

            Session["UserId"] = null;
            Session["UserName"] = null;
            Session["Email"] = null;
            Session["UserType"] = null;
        }
    }
}
